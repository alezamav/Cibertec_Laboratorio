using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebDeveloper.Repository
{
    //https://www.dropbox.com/sh/52jz7ofmekjo4zi/AABJNaUWBk0yruIUX5fMpsFva?dl=0
    //https://securityintelligence.com/the-10-most-common-application-attacks-in-action/
    //http://sqlzoo.net/hack/
    public interface IRepository<T>
    {
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
        List<T> GetList();
        T GetById(Expression<Func<T, bool>> match);
        IEnumerable<T> OrderedListByDateAndSize(Expression<Func<T, DateTime>> match, int size);
        IEnumerable<T> PaginatedList(Expression<Func<T, DateTime>> match,int page, int size);
    }
}
