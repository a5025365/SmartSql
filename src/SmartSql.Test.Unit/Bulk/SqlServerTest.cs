﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SmartSql.Bulk.SqlServer;
using SmartSql.Bulk;
using System.Threading.Tasks;
using SmartSql.Test.Entities;

namespace SmartSql.Test.Unit.Bulk
{
    public class SqlServerTest : AbstractXmlConfigBuilderTest
    {
        protected ISqlMapper SqlMapper => BuildSqlMapper(this.GetType().FullName);

        [Fact]
        public void Insert()
        {
            using (var dbSession= SqlMapper.SessionStore.Open())
            {
                var data = SqlMapper.GetDataTable(new RequestContext
                {
                    Scope = nameof(AllPrimitive),
                    SqlId = "Query",
                    Request = new { Taken = 100 }
                });
                data.TableName = "T_AllPrimitive";
                IBulkInsert bulkInsert = new BulkInsert(dbSession);
                bulkInsert.Table = data;
                bulkInsert.Insert();
            }           
        }
        [Fact]
        public void InsertByList()
        {
            using (var dbSession = SqlMapper.SessionStore.Open())
            {
                IBulkInsert bulkInsert = new BulkInsert(dbSession);
                var list = SqlMapper.Query<AllPrimitive>(new RequestContext
                {
                    Scope = nameof(AllPrimitive),
                    SqlId = "Query",
                    Request = new { Taken = 100 }
                });
                bulkInsert.Insert(list);
            }
        }
        [Fact]
        public async Task InsertAsync()
        {
            using (var dbSession = SqlMapper.SessionStore.Open())
            {
                var data = await SqlMapper.GetDataTableAsync(new RequestContext
                {
                    Scope = nameof(AllPrimitive),
                    SqlId = "Query",
                    Request = new {Taken = 100}
                });
                data.TableName = "T_AllPrimitive";
                IBulkInsert bulkInsert = new BulkInsert(dbSession);
                bulkInsert.Table = data;
                await bulkInsert.InsertAsync();
            }
        }
        [Fact]
        public async Task InsertByListAsync()
        {
            using (var dbSession = SqlMapper.SessionStore.Open())
            {
                var list = await SqlMapper.QueryAsync<AllPrimitive>(new RequestContext
                {
                    Scope = nameof(AllPrimitive),
                    SqlId = "Query",
                    Request = new {Taken = 100}
                });
                IBulkInsert bulkInsert = new BulkInsert(dbSession);
                await bulkInsert.InsertAsync<AllPrimitive>(list);
            }
        }
    }
}
