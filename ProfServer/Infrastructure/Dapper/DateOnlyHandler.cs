using System;
using System.Data;
using Dapper;

namespace ProfServer.Infrastructure.Dapper
{
    public class DateOnlyHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override DateOnly Parse(object value)
        {
            if (value is DateTime dt) return DateOnly.FromDateTime(dt);
            if (value is string s && DateOnly.TryParse(s, out var d)) return d;
            throw new DataException($"Cannot convert {value?.GetType()} to DateOnly");
        }

        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.DbType = DbType.Date;
            parameter.Value = value.ToDateTime(TimeOnly.MinValue);
        }
    }

    public class NullableDateOnlyHandler : SqlMapper.TypeHandler<DateOnly?>
    {
        public override DateOnly? Parse(object value)
        {
            if (value == null || value is DBNull) return null;
            if (value is DateTime dt) return DateOnly.FromDateTime(dt);
            if (value is string s && DateOnly.TryParse(s, out var d)) return d;
            throw new DataException($"Cannot convert {value?.GetType()} to DateOnly?");
        }

        public override void SetValue(IDbDataParameter parameter, DateOnly? value)
        {
            if (value == null)
            {
                parameter.Value = DBNull.Value;
                return;
            }
            parameter.DbType = DbType.Date;
            parameter.Value = value.Value.ToDateTime(TimeOnly.MinValue);
        }
    }
}