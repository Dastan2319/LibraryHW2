using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entity
{
    class MyContextInitializer : NullDatabaseInitializer<Model1>
    {
        public override void InitializeDatabase(Model1 db)
        {
            

            

            if(db.Ganre.Count()==0)
            {
                Ganre g1 = new Ganre { FirstName = "фантастика" };
                Ganre g2 = new Ganre { FirstName = "детектив" };
                Ganre g3 = new Ganre { FirstName = "триллер" };
                Ganre g4 = new Ganre { FirstName = "история" };

                db.Ganre.Add(g1);
                db.Ganre.Add(g2);
                db.Ganre.Add(g3);
                db.Ganre.Add(g4);

                db.SaveChanges();
            }
         



            
        }
    }
}
