using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProgramingLanguage : Entity
    {
        public string Name { get; set; }

        //Technology n
        public virtual ICollection<Technology> Technologies { get; set; }

        public ProgramingLanguage(int id,string name): this()
        {
            Id = id;
            Name = name;
        }

        public ProgramingLanguage()
        {

        }


    }
}
