﻿using Microsoft.EntityFrameworkCore;
using RestWithAspNetUdemy.Business.Base;
using RestWithAspNetUdemy.Model.Context;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNetUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected PostgreSqlContext _context;
        private DbSet<T> _dateSet;

        public GenericRepository(PostgreSqlContext context)
        {
            _context = context;
            _dateSet = _context.Set<T>();
        }

        public List<T> FindAll()
        {
            return _dateSet.ToList();
        }

        public T FindById(long id)
        {
            return _dateSet.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Create(T item)
        {
            try
            {
                _dateSet.Add(item);
                _context.SaveChanges();

                return item;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Delete(long id)
        {
            var result = _dateSet.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null) 
            {
                try
                {
                    _dateSet.Remove(result);
                    _context.SaveChanges();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }

        public T Update(T item)
        {
            var result = _dateSet.SingleOrDefault(p => p.Id.Equals(item.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();

                    return result;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public bool Exists(long id)
        {
            return _dateSet.Any(p => p.Id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return _dateSet.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = string.Empty;

            using(var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }

            return int.Parse(result);
        }
    }
}
