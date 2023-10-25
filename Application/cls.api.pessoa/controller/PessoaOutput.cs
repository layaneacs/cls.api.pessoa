using cls.api.pessoa.core.model;

namespace cls.api.pessoa.controller
{
    public class PessoaOutput<T> 
    {
        public T? Data { get; set; }    
        public Notificacao Notificacao { get; set; }

        public PessoaOutput()
        {
            Notificacao = new Notificacao();
        }       
    }
}
