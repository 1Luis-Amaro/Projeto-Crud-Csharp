namespace SalesWebMvc.Models.Enums
{
    
    // utilizado para representar o status de uma venda. A enumeração 'SalesStatus' será usada em outras partes do código 
    // para especificar o estado de uma venda, podendo ter um dos três valores definidos.
    public enum SalesStatus : int
    {
        // Valor 'Pending', representado pelo número 0. Este status indica que a venda está pendente.
        // Quando uma venda é criada, mas ainda não foi faturada ou cancelada, ela pode estar nesse status.
        Pending = 0,

        // Valor 'Billed', representado pelo número 1. Este status indica que a venda foi faturada.
        // Quando a venda é finalizada e o pagamento foi processado ou a nota fiscal foi gerada, ela entra nesse status.
        Billed = 1,

        // Valor 'Canceled', representado pelo número 2. Este status indica que a venda foi cancelada.
        // Esse status é usado quando a venda foi cancelada antes da conclusão do processo de faturamento ou pagamento.
        Canceled = 2
    }
}
