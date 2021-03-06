﻿using System;
using System.Collections.Generic;
using FluentMigrator.Infrastructure;

namespace FluentMigrator.Model
{
	public class ForeignKeyDefinition : ICloneable, ICanBeConventional, ICanBeValidated
	{
		public virtual string Name { get; set; }
		public virtual string ForeignTable { get; set; }
		public virtual string PrimaryTable { get; set; }
		public virtual ICollection<string> ForeignColumns { get; set; }
		public virtual ICollection<string> PrimaryColumns { get; set; }

		public ForeignKeyDefinition()
		{
			ForeignColumns = new List<string>();
			PrimaryColumns = new List<string>();
		}

		public void ApplyConventions(MigrationConventions conventions)
		{
			if (String.IsNullOrEmpty(Name))
				Name = conventions.GetForeignKeyName(this);
		}

		public virtual void CollectValidationErrors(ICollection<string> errors)
		{
			if (String.IsNullOrEmpty(Name))
				errors.Add(ErrorMessages.ForeignKeyNameCannotBeNullOrEmpty);

			if (String.IsNullOrEmpty(ForeignTable))
				errors.Add(ErrorMessages.ForeignTableNameCannotBeNullOrEmpty);

			if (String.IsNullOrEmpty(PrimaryTable))
				errors.Add(ErrorMessages.PrimaryTableNameCannotBeNullOrEmpty);

			if (!String.IsNullOrEmpty(ForeignTable) && !String.IsNullOrEmpty(PrimaryTable) && ForeignTable.Equals(PrimaryTable))
				errors.Add(ErrorMessages.ForeignKeyCannotBeSelfReferential);

			if (ForeignColumns.Count == 0)
				errors.Add(ErrorMessages.ForeignKeyMustHaveOneOrMoreForeignColumns);

			if (PrimaryColumns.Count == 0)
				errors.Add(ErrorMessages.ForeignKeyMustHaveOneOrMorePrimaryColumns);
		}

		public object Clone()
		{
			return new ForeignKeyDefinition
			{
				Name = Name,
				ForeignTable = ForeignTable,
				PrimaryTable = PrimaryTable,
				ForeignColumns = new List<string>(ForeignColumns),
				PrimaryColumns = new List<string>(PrimaryColumns)
			};
		}
	}
}