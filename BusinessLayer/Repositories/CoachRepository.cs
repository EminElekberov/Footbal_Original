using BusinessLayer.Repositories.Model;
using DataAccessLayer.Abstract;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class CoachRepository: ICoachRepository
    {
        private readonly ICoachesDal _coachesDal;

        public CoachRepository(ICoachesDal coachesDal)
        {
            _coachesDal = coachesDal;
        }

        public Coach GetById(int id)
        {
            return _coachesDal.GetById(id);
        }

        public Coach GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Coach> GetList()
        {
            return _coachesDal.GetAllList();
        }

        public void TAdd(Coach t)
        {
            _coachesDal.Insert(t);
        }

        public void TDelete(Coach t)
        {
            _coachesDal.Delete(t);
        }

        public void TUpdate(Coach t)
        {
            _coachesDal.Update(t);
        }
    }
}
