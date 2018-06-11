using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //好神奇的方法 DataContext [Table] 以前从来没有做过，居然只要弄一个类对应数据库中的表
            //然后，在前面弄一个[Table]特性，如果类名和表名不相同就：[Table (Name="Customers")]标记哦！
            //[Table]定义在Syste.Data.Linq.mapping命名空间下，使用前记得添加引用
            //而DataContext只要在创建时，在括号内放入连接字符串就行了！

            //data source=SUNLIGH-BVINTSS;initial catalog=Test;user id=sa;password=feng;
            DataContext dataContext = new DataContext("data source=SUNLIGH-BVINTSS;initial catalog=Test;user id=sa;password=feng;");
            Table<Customer> customers = dataContext.GetTable<Customer>();

            //查出表中姓名包含“a”并且按照长度来进行排序，最后转为大写
            IQueryable<string> query = from c in customers
                                       where c.Name.Contains("a")
                                       orderby c.Name.Length
                                       select c.Name.ToUpper();

            foreach (string name in query) Console.WriteLine(name);
            //LINQ to SQL把上面的查询翻译成如下的SQL语句：

            //SELECT UPPER([to].[Name]) AS[value]
            //FROM[Customer] AS[to]
            //WHERE[to].[Name] LIKE '%a%'
            //ORDER BY LEN([to].[Name])
        }
    }
    [Table]
    public class Customer
    {
        [Column(IsPrimaryKey = true)]
        public int ID;

        [Column]
        public string Name;
    }



}
