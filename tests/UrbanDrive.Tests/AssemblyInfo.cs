// O ConfiguradorSistema tem estado estático compartilhado (taxa e contador de veículos).
// Rodar as classes de teste em paralelo faria um teste alterar o valor que outro está conferindo.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
