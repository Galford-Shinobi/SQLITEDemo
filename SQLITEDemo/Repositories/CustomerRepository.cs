﻿using SQLite;
using SQLITEDemo.MVVM.Models;
using System.Linq.Expressions;

namespace SQLITEDemo.Repositories
{
    public class CustomerRepository
    {
        SQLiteConnection connection;
        public string StatusMessage { get; set; }

        public CustomerRepository()
        {
            connection =
                 new SQLiteConnection(Constants.DatabasePath,
                 Constants.Flags);
            connection.CreateTable<CustomerModel>();
        }

        public void AddOrUpdate(CustomerModel customer)
        {
            int result = 0;
            try
            {
                if (customer.ID != 0)
                {
                    result =
                         connection.Update(customer);
                    StatusMessage =
                         $"{result} row(s) updated";
                }
                else
                {
                    result = connection.Insert(customer);
                    StatusMessage =
                         $"{result} row(s) added";
                }

            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
        }

        public List<Customer> GetAll()
        {
            try
            {
                return connection.Table<Customer>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
            return null;
        }

        public List<Customer> GetAll(Expression<Func<Customer, bool>> predicate)
        {
            try
            {
                return connection.Table<Customer>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
            return null;
        }

        public List<Customer> GetAll2()
        {
            try
            {
                return connection.Query<Customer>("SELECT * FROM Customers").ToList();
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
            return null;
        }

        public CustomerModel Get(int id)
        {
            try
            {
                return
                     connection.Table<CustomerModel>()
                     .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
            return null;
        }

        public void Delete(int customerId)
        {
            try
            {
                var customer =
                     Get(customerId);
                connection.Delete(customer);
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
        }
    }
}
