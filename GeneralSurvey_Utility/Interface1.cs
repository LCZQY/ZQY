using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralSurvey_Utility
{


    public interface Interface1<T> where T : class, new()
    {

        /// <summary>
        /// 单个插入，返回影响行数
        /// </summary>
        /// <param name="topicgroups"></param>
        /// <returns></returns>
        int Insert(T model);

        /// <summary>
        ///   批量插入，返回影响行数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(List<T> model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="topicgroups"></param>
        /// <returns></returns>
        bool Update(T model);


        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(List<T> model);

        /// <summary>
        /// 查询指定数据
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        List<T> GetList(string cond);


        /// <summary>
        /// 无条件查询
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        List<T> GetList();


        /// <summary>
        /// 查询in操作
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<T> QueryIn(int[] ids, string cond = "id");





    }
}
