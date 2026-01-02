namespace Herval.DownloadArquivos.Domain.Entities
{
    public class FakeEntity
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        
        private FakeEntity()
        { }

        public FakeEntity(int id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
