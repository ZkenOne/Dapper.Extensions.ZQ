﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extensions.ZQ
{
    /// <summary>
    /// 分页参数模型
    /// </summary>
    public class PageParameters
    {
        /// <summary>
        /// 多表连接时,可以用此参数
        /// a.id,a.Name,b.sex,c.Flag from tb_a as a left join tb_b as b on b.id=a.id
        /// 失效属性 OrderBy 
        /// 失效属性 TableName
        /// Where字段填写条件 如:Where 1=1
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 条件 where 如:Where 1=1
        /// </summary>
        public string Where { get; set; } = string.Empty;

        /// <summary>
        /// 表名(Sql为空并且此值为空则使用BaseDAL传入的TModel映射的表名)
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 利用某字段进行分页 一般主键 也可以先排序在分页 格式 id desc 
        /// <para>默认ID</para>
        /// ROW_NUMBER() OVER(ORDER BY {p.KeyFiled}) 
        /// </summary>
        public string KeyFiled { get; set; } = "ID";

        /// <summary>
        /// 需要显示的字段
        /// <para>默认全部</para>
        /// </summary>
        public string ShowField { get; set; } = "*";

        /// <summary>
        /// 排序字段(省略 order by)如: id desc,没有值取 KeyFiled 属性值
        /// </summary>
        public string OrderBy { get; set; } = "";

        /// <summary>
        /// 当前第几页
        /// <para>默认1</para>
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页分多少条数据
        /// <para>默认10条</para>
        /// </summary>
        public int PageSize { get; set; } = 10;
    }

    /// <summary>
    /// 分页数据模型
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class PageData<T> where T : class, new()
    {
        /// <summary>
        /// 分页总数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// 用于统计
        /// </summary>
        public object Totals { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>

        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 数据总数
        /// </summary>
        public long DataCount { get; set; }
    }
}