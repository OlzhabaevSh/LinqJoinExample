using System;
using System.Collections.Generic;
using System.Linq;
using LinqJoinExample.Models;

namespace LinqJoinExample
{
    class Program
    {

        static void Main(string[] args)
        {
            var manager = new JoinManager();

            manager.InnerJoin();
            manager.LeftJoin();
            manager.LeftExcludingJoin();
            manager.OuterJoin();
            manager.OuterExcludingJoin();

        }
    }
}
