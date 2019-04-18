﻿using SmartSql.Configuration.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SmartSql.Test.Unit.Tags
{
    public class IsGreaterThanTest : AbstractXmlConfigBuilderTest
    {
        protected ISqlMapper SqlMapper => BuildSqlMapper(this.GetType().FullName);

        [Fact]
        public void IsGreaterThan()
        {
            var msg = SqlMapper.ExecuteScalar<String>(new RequestContext
            {
                Scope = nameof(IsGreaterThanTest),
                SqlId = "IsGreaterThan",
                Request = new { Id = 11 }
            });
            Assert.Equal("Id IsGreaterThan 10", msg);
        }
        [Fact]
        public void IsGreaterThan_Required()
        {
            var msg = SqlMapper.ExecuteScalar<String>(new RequestContext
            {
                Scope = nameof(IsGreaterThanTest),
                SqlId = "IsGreaterThan_Required",
                Request = new { Id = 11 }
            });
            Assert.Equal("Id IsGreaterThan 10", msg);
        }
        [Fact]
        public void IsGreaterThan_Required_Fail()
        {
            try
            {
                var msg = SqlMapper.ExecuteScalar<String>(new RequestContext
                {
                    Scope = nameof(IsGreaterThanTest),
                    SqlId = "IsGreaterThan_Required",
                    Request = new { }
                });
                Assert.Equal("Id IsGreaterThan 10", msg);
            }
            catch (TagRequiredFailException ex)
            {
                Assert.True(true);
            }
        }
    }
}
