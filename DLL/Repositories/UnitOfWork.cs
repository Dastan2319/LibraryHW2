﻿using DLL;
using DLL.Entity;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Model1 db = new Model1();
        private BookRepository bookRepository;
        private UsersRepository usersRepository;
        private GanreRepository ganreRepository;
        private MessageRepository messageRepository;

        public IRepository<Books> Books        
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }

        public IRepository<Users> Users
        {
            get
            {
                if (usersRepository == null)
                    usersRepository = new UsersRepository(db);
                return usersRepository;
            }
        }
        

        public IRepository<Ganre> Ganre
        {
            get
            {
                if (ganreRepository == null)
                    ganreRepository = new GanreRepository(db);
                return ganreRepository;
            }
        }

        public IRepository<Message> Message
        {
            get
            {
                if (messageRepository == null)
                    messageRepository = new MessageRepository(db);
                return messageRepository;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}