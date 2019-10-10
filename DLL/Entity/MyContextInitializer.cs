using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entity
{
    class MyContextInitializer : CreateDatabaseIfNotExists<Model1>
    {
        protected override void Seed(Model1 db)
        {
            Authors a1 = new Authors {FirstName="TestAuthor",LastName="asd" };
            Authors a2 = new Authors { FirstName = "TestAuthor2", LastName = "zxc" };
      
            Ganre g1 = new Ganre { FirstName = "TestGanre1" };
            Ganre g2 = new Ganre { FirstName = "TestGanre2" };
   

            Users u1 = new Users { FIO = "UsersAuthor" };
            Users u2 = new Users { FIO = "UsersAuthor2" };
    

            Books b1 = new Books { Images = "img1", Pages = 23,Price=23,Title="asdsa" };
            Books b2 = new Books { Images = "img2", Pages = 51, Price = 45, Title = "qwe" };

            db.Books.Add(b1);
            db.Books.Add(b2);

            db.Authors.Add(a1);
            db.Authors.Add(a2);

            db.Ganre.Add(g1);
            db.Ganre.Add(g2);


            db.Users.Add(u1);
            db.Users.Add(u2);

            db.SaveChanges();
        }
    }
}
