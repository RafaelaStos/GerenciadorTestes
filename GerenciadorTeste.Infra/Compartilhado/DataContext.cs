using GerenciadorTeste.Dominio.ModuloDisciplina;
using System;
using System.Linq;
using System.Collections.Generic;

namespace GerenciadorTeste.Infra
{
    [Serializable]

    public class DataContext
    {
        private ISerializador serializador;

        public DataContext()
        {
            Disciplinas = new List<Disciplina>();
        }

        public DataContext(ISerializador serializador): this()
        {
            this.serializador = serializador;
            CarregarDados();
        }

        
        public List<Disciplina> Disciplinas { get; set; }

        public void GravarDados()
        {
            serializador.GravarDadosEmArquivo(this);
        }

        private void CarregarDados()
        {
            var ctx = serializador.CarregarDadosDoArquivo();

            if (ctx.Disciplinas.Any())
                this.Disciplinas.AddRange(ctx.Disciplinas);

        }
    }
}