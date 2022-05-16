
using FluentValidation;
using FluentValidation.Results;
using GerenciadorTeste.Dominio;
using GerenciadorTeste.Dominio.ModuloDisciplina;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTeste.Infra.ModuloDisciplina
{
    public class RepositorioDisciplinaEmArquivos : RepositorioEmArquivoBase<Disciplina>, IRepositorioDisciplina
    {
        
        public RepositorioDisciplinaEmArquivos(DataContext contextoDados) : base(contextoDados)
        {
            if (dataContext.Disciplinas.Count > 0)
            
            contador = dataContext.Disciplinas.Max(x => x.Id);

        }

        public override ValidationResult Inserir(Disciplina novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid)
            {
                novoRegistro.Id = ++contador;

                var registros = ObterRegistros();

                registros.Add(novoRegistro);
            }

            return resultadoValidacao;
        }

        public virtual ValidationResult Editar(Disciplina registro)
        {
            var resultadoValidacao = Validar(registro);

            if (resultadoValidacao.IsValid)
            {
                var registros = ObterRegistros();

                foreach (var item in registros)
                {
                    if (item.Id == registro.Id)
                    {
                        item.Atualizar(registro);
                        break;
                    }
                }
            }

            return resultadoValidacao;
        }

        
        public override List<Disciplina> ObterRegistros()
        {
            return dataContext.Disciplinas;
        }

        public override AbstractValidator<Disciplina> ObterValidador()
        {
            return new ValidadorDisciplina();
        }

        private ValidationResult Validar(Disciplina registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = ValidarNome(registro);

            return resultadoValidacao;
        }

        private ValidationResult ValidarNome(Disciplina registro)
        {
            bool nomeRegistrado = VerificarSeOhNomeJaEstaRegistrado(registro);

            ValidationResult validacaoDeNome = new ValidationResult();

            if (nomeRegistrado)
            {
                if (registro.Id == 0)
                    validacaoDeNome.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe uma disciplina com este nome cadastrada no sistema!"));

                else if (ObterDisciplinaPeloNome(registro.Nome).Id != registro.Id)
                    validacaoDeNome.Errors.Add(new ValidationFailure("", "Não foi possível editar, pois já existe uma disciplina com este nome cadastrada no sistema!"));
            }
            return validacaoDeNome;
        }

        private bool VerificarSeOhNomeJaEstaRegistrado(Disciplina registro)
        {
            return ObterRegistros()
                           .Select(x => x.Nome.ToUpper())
                           .Contains(registro.Nome.ToUpper());
        }

        private Disciplina ObterDisciplinaPeloNome(string nome)
        {
            return ObterRegistros()
               .Find(x => x.Nome.ToUpper() == nome.ToUpper());
        }
    }
}
