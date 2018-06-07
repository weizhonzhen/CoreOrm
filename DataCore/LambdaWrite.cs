﻿using Data.Core.Base;
using Data.Core.Context;
using Data.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Core
{
    /// <summary>
    /// 标签：2015.9.6，魏中针
    /// 说明：数据库操作类
    /// </summary>
    public static class LambdaWrite
    {
        #region 批量增加
        /// <summary>
        /// 批量增加
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="model">实体</param>
        /// <param name="IsTrans">是否事务</param>
        /// <returns></returns>
        public static WriteReturn AddList<T>(List<T> list, DataContext db = null, string key = null) where T : class, new()
        {
            ConfigModel config = null;
            var result = new DataReturn<T>();
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            if (db == null)
            {
                var tempDb = BaseContext.GetContext(key);
                result = tempDb.AddList<T>(list);
                config = tempDb.config;
                tempDb.Dispose();
            }
            else
            {
                result = db.AddList<T>(list);
                config = db.config;
            }

            stopwatch.Stop();

            Task.Factory.StartNew(() =>
            {
                DbLog.LogSql(config.IsOutSql, result.sql, config.DbType, stopwatch.Elapsed.TotalMilliseconds);
            });

            return result.writeReturn;
        }
        #endregion

        #region 批量增加 asy
        /// <summary>
        /// 批量增加 asy
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="model">实体</param>
        /// <param name="IsTrans">是否事务</param>
        /// <returns></returns>
        public static async Task<WriteReturn> AddListAsy<T>(List<T> list, DataContext db = null, string key = null) where T : class, new()
        {
            return await Task.Factory.StartNew(() =>
            {
                return AddList<T>(list, db, key);
            });
        }
        #endregion


        #region 增加
        /// <summary>
        /// 增加
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="model">实体</param>
        /// <param name="IsTrans">是否事务</param>
        /// <param name="notAddField">不需要增加的字段</param>
        /// <returns></returns>
        public static WriteReturn Add<T>(T model, DataContext db = null, string key = null) where T : class, new()
        {
            ConfigModel config = null;
            var result = new DataReturn<T>();
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            if (db == null)
            {
                var tempDb = BaseContext.GetContext(key);
                result = tempDb.Add<T>(model, false);
                config = tempDb.config;
                tempDb.Dispose();
            }
            else
            {
                result = db.Add<T>(model, false);
                config = db.config;
            }

            stopwatch.Stop();

            Task.Factory.StartNew(() =>
            {
                DbLog.LogSql(config.IsOutSql, result.sql, config.DbType, stopwatch.Elapsed.TotalMilliseconds);
            });
            return result.writeReturn;
        }
        #endregion

        #region 增加 asy
        /// <summary>
        /// 增加 asy
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="model">实体</param>
        /// <param name="IsTrans">是否事务</param>
        /// <param name="notAddField">不需要增加的字段</param>
        /// <returns></returns>
        public static async Task<WriteReturn> AddAsy<T>(T model, DataContext db = null, string key = null) where T : class, new()
        {
            return await Task.Factory.StartNew(() =>
            {
                return Add<T>(model, db, key);
            });
        }
        #endregion


        #region 删除(Lambda表达式)
        /// <summary>
        /// 删除(Lambda表达式)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="IsTrans">是否事务</param>
        /// <returns></returns>
        public static WriteReturn Delete<T>(Expression<Func<T, bool>> predicate, DataContext db = null, string key = null) where T : class, new()
        {
            ConfigModel config = null;
            var result = new DataReturn<T>();
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            if (db == null)
            {
                var tempDb = new DataContext(key);
                result = tempDb.Delete<T>(predicate);
                config = tempDb.config;
                tempDb.Dispose();
            }
            else
            {
                result = db.Delete<T>(predicate);
                config = db.config;
            }

            stopwatch.Stop();

            Task.Factory.StartNew(() =>
            {
                DbLog.LogSql(config.IsOutSql, result.sql, config.DbType, stopwatch.Elapsed.TotalMilliseconds);
            });
            return result.writeReturn;
        }
        #endregion

        #region 删除(Lambda表达式)asy
        /// <summary>
        /// 删除(Lambda表达式)asy
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="IsTrans">是否事务</param>
        /// <returns></returns>
        public static async Task<WriteReturn> DeleteAsy<T>(Expression<Func<T, bool>> predicate, DataContext db = null, string key = null) where T : class, new()
        {
            return await Task.Factory.StartNew(() =>
            {
                return Delete<T>(predicate, db, key);
            });
        }
        #endregion


        #region 修改(Lambda表达式)
        /// <summary>
        /// 修改(Lambda表达式)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="model">实体</param>
        /// <param name="predicate">表达式</param>
        /// <param name="IsTrans">是否事务</param>
        /// <param name="field">需要修改的字段</param>
        /// <returns></returns>
        public static WriteReturn Update<T>(T model, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> field = null, DataContext db = null, string key = null) where T : class, new()
        {
            ConfigModel config = null;
            var result = new DataReturn<T>();
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            if (db == null)
            {
                var tempDb = BaseContext.GetContext(key);
                result = tempDb.Update<T>(model, predicate, field);
                config = tempDb.config;
                tempDb.Dispose();
            }
            else
            {
                result = db.Update<T>(model, predicate, field);
                config = db.config;
            }

            stopwatch.Stop();

            Task.Factory.StartNew(() =>
            {
                DbLog.LogSql(config.IsOutSql, result.sql, config.DbType, stopwatch.Elapsed.TotalMilliseconds);
            });

            return result.writeReturn;
        }
        #endregion

        #region 修改(Lambda表达式)asy
        /// <summary>
        /// 修改(Lambda表达式)asy
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="model">实体</param>
        /// <param name="predicate">表达式</param>
        /// <param name="IsTrans">是否事务</param>
        /// <param name="field">需要修改的字段</param>
        /// <returns></returns>
        public static async Task<WriteReturn> UpdateAsy<T>(T model, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> field = null, DataContext db = null, string key = null) where T : class, new()
        {
            return await Task.Factory.StartNew(() =>
            {
                return Update<T>(model, predicate, field, db, key);
            });
        }
        #endregion


        #region 执行sql
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static WriteReturn ExecuteSql(string sql, DbParameter[] param, DataContext db = null, string key = null)
        {
            ConfigModel config = null;
            var result = new DataReturn();
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            if (db == null)
            {
                var tempDb = BaseContext.GetContext(key);
                result = tempDb.ExecuteSql(sql, param,true);
                config = tempDb.config;
                tempDb.Dispose();
            }
            else
            {
                result = db.ExecuteSql(sql, param,true);
                config = db.config;
            }

            stopwatch.Stop();

            Task.Factory.StartNew(() => { DbLog.LogSql(config.IsOutSql, result.Sql, config.DbType, stopwatch.Elapsed.TotalMilliseconds); });

            return result.writeReturn;
        }
        #endregion

        #region 执行sql asy
        /// <summary>
        /// 执行sql asy
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<WriteReturn> ExecuteSqlAsy(string sql, DbParameter[] param, DataContext db = null, string key = null)
        {
            return await Task.Factory.StartNew(() =>
            {
                return ExecuteSql(sql, param, db, key);
            });
        }
        #endregion
    }
}

