
using GerenciadorTeste.Dominio.Compartilhado;
using System;

namespace GerenciadorTeste.Dominio.ModuloDisciplina
{
    [Serializable]
    public class Disciplina : EntidadeBase<Disciplina>
    {
       
        public Disciplina()
        {
        }

        public Disciplina(string n) : this()
        { 
          
            Nome = n;

        }

        public string Nome { get; set; }

        public override string ToString()
        {


            return $"ID: {Id}, Disciplina: {Nome}";
        }

        public override void Atualizar(Disciplina registro)
        {
            this.Nome = registro.Nome;  
        }
    }
}