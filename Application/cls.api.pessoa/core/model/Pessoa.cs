namespace cls.api.pessoa.core.model
{
    public class Pessoa
    {
        public Guid Id { get; private set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa(string nome, string sobrenome, string email, string telefone, DateTime dataNascimento)
        {
            Id= Guid.NewGuid();
            Nome= nome;
            Sobrenome= sobrenome;
            Email = email;
            Telefone= telefone;
            DataNascimento= dataNascimento;            
        }
        public Pessoa()
        {
            Id = Guid.NewGuid();
        }        
    }
}
