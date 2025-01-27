# Documento de Visão
> Projeto de uma aplicação real com conceitos da disciplina de POO(Programação Orientada a Objeto)

## Loja Virtual de Jogos 

### 📌Histórico da Revisão 

| Data📅  | Versão💻 | Descrição📕 | Autor(es)🧑🏻‍💻|
|:-------|:-------|:----------|:------|
| 26/01/2025 |  **1.0** | Versão Inicial  | [_Gabriel Henrique Barbosa_](https://github.com/gbrielf) e [_João Vitor Bezerra_](https://github.com/DevJoaoVitorB) |

### 1. Objetivo do Projeto 

> O Projeto _Loja Virtual de Jogos_ busca levar praticidade ao público apreciador de jogos com um ambiente seguro para compra de jogos e consoles da nova e antiga geração de multiplataformas.

### 2. Descrição do Problema 

| Perguntas | Respostas |
|:------|:------|
| *Qual é o Problema?* | 📌Dificuldade de encontrar jogos e consoles de nova e antiga geração com preços de qualidade e acessíveis em aplicações confiáveis |
| *Quem são os Afetados?* | 📌Apreciadores de jogos e público de conhecimento novo do mundo do entreterimento gamer |
| *Quais os Impactos?* | 📌Desistímulo de mercado e baixa na compra de jogos e consoles de nova e velha geração como consequência da "fuga" desses públicos |
| *Qual a Solução?* | 📌Um ambiente virtual e seguro que oferece um serviço de qualidade com ótimos preços, promoções e entrega rápida de jogos e de consoles de nova e antiga geração |

### 3. Descrição dos Usuários

| Nome | Descrição | Responsabilidades |
|:---  |:--- |:--- |
| *Administrador* | 📌Realiza as atividades relacionadas a manuteção do sistema | 📌Controle de logística: manter categorias, estoques de produtos, finaceiro e cadastro de clientes sempre atualizados |
| *Cliente* | 📌Realiza as atividades relacionadas a compra dos produtos oferecidos | 📌Realiza o próprio cadastro no sistema; Adicionar os produtos de sua preferência ao carrinho e realizar a compra |

### 4. Descrição do Ambiente dos Usuários

O cenário de entreterimento virtual possui como um de seus âmagos os jogos virtuais multiplataformas, os quais possui um público muito ativo e que buscam sempre novidades e melhorias de hardware, com desempenho superior aos obtidos anteriomente. No entanto, com as melhorias, os custos aumentam afetando assim uma parcela do público que não possui condições necessárias para obter essas novidades. E, também, aqueles que por não terem o entreterimento virtual como principal hobby acabam deixando-o de lado, devido aos altos preços vigentes no mercado. Dessa forma, a _Loja Virtual de Jogos_ busca construir um ambiente seguro de compra com preços mais acessíveis para jogos e consoles e seus diversos gostos. Assim, é possível agregar o público que antes estava impossibilitado de participar da nova geração gamer.

A nostalgia por trás dos jogos antigos também é um dos motores que move o cenário de jogos eletrônicos. Muitos jogadores preferem uma experiência retrô com jogos que marcaram sua infância, e que fizeram daquele momento especial. Em virtude disso, a _Loja Virtual de Jogos_, além de corresponder as expectativas do público da nova geração gamer, eleva a experiência do público da velha guarda com uma experiência rêtro e nostálgica com jogos e consoles antigos com preços abaixo do mercado.

### 5. Principais Necessidades dos Usuários

Para empresas e profissionais, a necessidade é divulgar sua disponibilidade de atendimentos para viabilizar, de forma mais eficiente, o atendimento dos seus clientes.

Para os clientes, as necessidades são encontrar profissionais e empresas prestadoras de serviço e agendar atendimentos com estes de acordo as disponibilidades de tempo dos envolvidos.

### 6.	Alternativas Concorrentes

As alternativas concorrentes são, em geral, específicas para uma empresa ou profissional. A ideia do sistema proposto é prover uma solução simples, acessível e padronizada para o agendamento de serviços e que pode ser utilizada por quaisquer profissionais e empresas.

### 7.	Visão Geral do Produto

Em resumo, o sistema de Agendamento de Serviços é uma aplicação que permite empresas e profissionais registrarem suas disponibilidades de atendimento aos seus clientes, de forma que estes possam consultar e agendar horários para realização de serviços.

O sistema deve ter uma interface amigável e permitir o acesso concorrente de clientes para agendamento de um horário de atendimento.

### 8. Requisitos Funcionais

| Código | Nome | Descrição |
|:---  |:--- |:--- |
| RF01 | Entrar no sistema | Usuários devem logar no sistema para acessar as funcionalidades relacionadas ao agendamento |
| RF02 | Cadastro de Funcionários | Administrador do sistema mantém o cadastro dos funcionários responsáveis pelo gerenciamento das agendas |
| RF03 | Gerenciamento de Serviços |  Funcionário mantém a relação de serviços prestados pela empresa ou profissional |
| RF04 | Gerenciamento da Agenda | Funcionário registra os horários disponíveis de atendimento, confirma e cancela o agendamento de clientes |
| RF05 | Cadastro de Clientes | Cliente deve realizar o auto cadastramento |
| RF06 | Consulta de Agendas | Cliente consulta agendas de atendimento dos serviços disponíveis, podendo agendar um serviço  |
| RF07 | Consulta de Agendamento | Cliente consulta atendimentos agendados, podendo cancelar um agendamento |


### 9. Requisitos Não-funcionais

 Código | Nome | Descrição | Categoria | Classificação
|:---  |:--- |:--- |:--- |:--- |
| RNF01 | Design responsivo | O sistema deve adaptar-se a qualquer tamanho de tela de dispositivo, seja, computador, tablets ou smart phones. | Usabilidade| Obrigatório |
| RNF02 | Criptografia de dados| Senhas de usuários devem ser gravadas de forma criptografada no banco de dados. | Segurança | Obrigatório |
| RNF03 | Controle de acesso | Só usuários autenticados podem ter acesso ao sistema, com exceção ao auto cadastramento do usuário. | Segurança | Obrigatório |
| RNF04 | Tempo de resposta |A comunicação entre o servidor e o cliente deve ocorrer em tempo hábil | Performance | Desejável |
| RNF05 | Sistema Web | A aplicação deve ser um site. | Arquitetura | Obrigatório |
| RNF06 | Dados pessoais | Os clientes não devem visualizar dados de outros clientes (na agenda, por exemplo). | Privacidade | Obrigatório |
