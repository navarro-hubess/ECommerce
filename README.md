# ECommerce
- Projeto com APIs para Cálculo de Montante de Juros Compostos passados como parâmetros: Valor Inicial, Tempo e Taxa de Juros.
- Esta API consome a API do Projeto (https://github.com/navarro-hubess/ECommerce.Taxas) para obtenção da Taxa de Juros.
 

## Conceitos, Frameworks e tecnologias utilizadas:
- em ASP.NET Core, C#, Clean Code, Rich Domain (DDD)
- Documentação das Apis com Swagger
1. Utilização de GIT com 2 Branchs 
  - Master e Homolog
- Injeção de Dependência
- Log usando Serilog 
(Projeto Ecommerce => https://github.com/navarro-hubess/ECommerce)
- Testes de Unidade e de Integração 
- Resiliência com Polly
 Foi adicionada uma política de Retry no projeto (ECommerce - https://github.com/navarro-hubess/ECommerce)
- Para o Cálculo do valorFinal de Juros foi usada a biblioteca https://www.nuget.org/packages/DecimalMath.DecimalEx/ para que fosse possível manter a precisão em decimal, já que a biblioteca original Microsoft Math.Pow somente aceita double.

## Get Started
> Para executar localmente:
- Descompactar e abrir o projeto no Visual Studio (recomenda-se 2019).
- Executar o projeto (F5) e o mesmo abrirá na documentação da API com Swagger.
- Dentro do Swagger seguir o fluxo para teste da Api
