using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations
{
    /*
     * This interface is just for documentation purposes, since our static xxxOperation
     * classes can not inherit nor implement classes or interfaces (language issue). 
     * 
     * Responsibilities:
     * 1) Authorize operation
     * 2) Validate entity
     * 3) Perform operation w/o SaveChanges()
     * 
     * Possible exceptions:
     * - ServerAuthorizeException = Requester not authorized to perform operation
     * - ServerValidateException  = Entity has invalid field format
     * - ServerConflictException  = Entity duplicated or may not be deleted
     * - ServerDBEntityException  = Entity to operate on is not in database
     */
    public interface IOperation
    {
        bool             CanRender(Account requester);
        void             TryCreate(Account requester, DataContext context, Type item);
        IQueryable<Type> TryList  (Account requester, DataContext context);
        Type             TryRead  (Account requester, DataContext context, int id);
        Type             TryUpdate(Account requester, DataContext context, Type dbentity);
        void             TryDelete(Account requester, DataContext context, int id);
    }
}
