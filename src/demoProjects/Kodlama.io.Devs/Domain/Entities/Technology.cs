using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Technology : Entity
    {
        public string Name { get; set; }

        //ProgramingLanguage 1
        public virtual ProgramingLanguage? ProgramingLanguage { get; set; }

        public int ProgramingLanguageId { get; set; }

        public Technology()
        {

        }

        public Technology(int id, string name, int programingLanguageId) : this()
        {
            Id = id;
            Name = name;
            ProgramingLanguageId = programingLanguageId;
        }
    }
}
