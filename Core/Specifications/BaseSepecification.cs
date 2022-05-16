using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSepecification<T> : ISpecification<T>
    {
        public BaseSepecification()
        {
            
        }
        
        public BaseSepecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();

        public void AddInclude(Expression<Func<T, object>> expression)
        {
            Includes.Add(expression);
        }
    }
}