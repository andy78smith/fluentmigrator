﻿using System;
using System.Data;
using System.Text;
using FluentMigrator.Model;

namespace FluentMigrator.Runner.Dialects
{
	public class SqlServer2005Dialect : DialectBase
	{
		public const int AnsiStringCapacity = 8000;
		public const int AnsiTextCapacity = 2147483647;
		public const int UnicodeStringCapacity = 4000;
		public const int UnicodeTextCapacity = 1073741823;
		public const int ImageCapacity = 2147483647;
		public const int DecimalCapacity = 19;
		public const int XmlCapacity = 1073741823;

		public SqlServer2005Dialect()
		{
			SetTypeMap(DbType.AnsiStringFixedLength, "CHAR(255)");
			SetTypeMap(DbType.AnsiStringFixedLength, "CHAR($size)", AnsiStringCapacity);
			SetTypeMap(DbType.AnsiString, "VARCHAR(255)");
			SetTypeMap(DbType.AnsiString, "VARCHAR($size)", AnsiStringCapacity);
			SetTypeMap(DbType.AnsiString, "TEXT", AnsiTextCapacity);
			SetTypeMap(DbType.Binary, "VARBINARY(8000)");
			SetTypeMap(DbType.Binary, "VARBINARY($size)", AnsiStringCapacity);
			SetTypeMap(DbType.Binary, "IMAGE", ImageCapacity);
			SetTypeMap(DbType.Boolean, "BIT");
			SetTypeMap(DbType.Byte, "TINYINT");
			SetTypeMap(DbType.Currency, "MONEY");
			SetTypeMap(DbType.Date, "DATETIME");
			SetTypeMap(DbType.DateTime, "DATETIME");
			SetTypeMap(DbType.Decimal, "DECIMAL(19,5)");
			SetTypeMap(DbType.Decimal, "DECIMAL(19,$size)", DecimalCapacity);
			SetTypeMap(DbType.Double, "DOUBLE PRECISION");
			SetTypeMap(DbType.Guid, "UNIQUEIDENTIFIER");
			SetTypeMap(DbType.Int16, "SMALLINT");
			SetTypeMap(DbType.Int32, "INT");
			SetTypeMap(DbType.Int64, "BIGINT");
			SetTypeMap(DbType.Single, "REAL");
			SetTypeMap(DbType.StringFixedLength, "NCHAR(255)");
			SetTypeMap(DbType.StringFixedLength, "NCHAR($size)", UnicodeStringCapacity);
			SetTypeMap(DbType.String, "NVARCHAR(255)");
			SetTypeMap(DbType.String, "NVARCHAR($size)", UnicodeStringCapacity);
			SetTypeMap(DbType.String, "NTEXT", UnicodeTextCapacity);
			SetTypeMap(DbType.Time, "DATETIME");
			SetTypeMap(DbType.Xml, "XML", XmlCapacity);
		}

		public override string GenerateDDLForColumn(ColumnDefinition column)
		{
			var sb = new StringBuilder();

			sb.Append("(");
			sb.Append(column.Name);
			sb.Append(" ");
			sb.Append(GetTypeMap(column.Type.Value, column.Size, column.Precision));

			// TODO (nkohari) -- not finished

			return sb.ToString();
		}
	}
}