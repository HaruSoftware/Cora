# PRÉ-PROJETO DE TRABALHO DE GRADUAÇÃO (TG)

**Curso:** Análise e Desenvolvimento de Sistemas (ADS)  
**Instituição:** FATEC Franca – Dr. Thomaz Novelino  
**Nome do Projeto:** Cora  
**Orientadore: Carlos Eduardo França Roland 

---

## 1. Definição do Tema

* **Tema:** Desenvolvimento de um aplicativo multiplataforma de gestão comercial simplificada e engajamento de clientes para micro e pequenos comércios varejistas.
* **Pertinência, Relevância e Viabilidade:** O tema está diretamente alinhado às disciplinas de Engenharia de Software, Programação de Dispositivos Móveis, Banco de Dados e Gestão de Negócios do curso de ADS. Sua relevância técnica consiste na integração de bancos de dados relacionais e segurança de dados em uma interface mobile simples. Socialmente e economicamente, o projeto é viável pois foca em micro e pequenas empresas (que frequentemente operam sem sistemas automatizados por conta de custos e complexidade), promovendo inclusão digital e aumento de competitividade local.

---

## 2. Questão Problema do Projeto

> *Como o desenvolvimento de um aplicativo de gestão pode otimizar os processos de pequenos comércios e melhorar a experiência de seus clientes?*

---

## 3. Refinamento da Questão Problema do Projeto

*Processo de especificação da questão de pesquisa em etapas sucessivas para torná-la mais clara:*

* **3.1.** De que forma uma solução de software integrada pode auxiliar pequenos comerciantes a organizar suas vendas presenciais e aproximar os clientes de seus estabelecimentos?
* **3.2.** Como um aplicativo móvel focado em usabilidade e simplicidade pode resolver a falta de controle de estoque de comerciantes e facilitar a comunicação de ofertas aos consumidores?
* **3.3.** Como o desenvolvimento de um aplicativo mobile que integre controle básico de fluxo de caixa, estoque e um canal interativo de feedbacks pode elevar a eficiência de microempresas e aumentar a fidelidade do cliente local?
* **3.4.** Como um aplicativo multiplataforma focado na automatização de rotinas básicas de estoque e vendas, integrado a um painel de consulta e feedback para clientes, pode reduzir a ineficiência operacional de lojistas locais e otimizar a experiência de compra dos consumidores finais?

---

## 4. Questão Problema do Projeto - Final

> **Como um aplicativo multiplataforma focado na automatização de rotinas básicas de estoque e vendas, integrado a um painel de consulta e feedback para clientes, pode reduzir a ineficiência operacional de lojistas locais e otimizar a experiência de compra dos consumidores finais?**

---

## 5. Justificativas do Projeto

* **Contextualização e Storytelling:** Imagine a rotina de "Dona Maria", dona de uma mercearia de bairro. Ela concilia o atendimento direto ao cliente com o controle manual de estoque em cadernos e planilhas confusas. Frequentemente, produtos acabam sem que ela perceba a tempo, e clientes saem frustrados pela falta de itens essenciais. Do outro lado do balcão, o cliente moderno, acostumado com a rapidez da tecnologia, se depara com filas e falta de canais práticos para dar sugestões ou consultar preços.
* **Justificativa Técnica e Social:** O projeto **Cora** surge como a hipótese de solução para esse cenário, unindo a organização de processos internos com a melhoria da experiência de compra. A importância social reside na democratização tecnológica de lojistas que não possuem orçamento para licenciar ERPs pesados e caros, garantindo a sustentabilidade de negócios locais. Tecnicamente, o projeto se justifica pelo estudo empírico de padrões de design de interface (UI/UX) focados em públicos de baixa familiaridade com a tecnologia, além do estudo de arquiteturas de software escaláveis (como APIs REST e bancos de dados em nuvem). Os recursos técnicos necessários (frameworks de código aberto e servidores com camadas de teste gratuitas) tornam o desenvolvimento financeiramente viável e executável dentro das capacidades propostas pelo curso de ADS.

---

## 6. Objetivo Geral do Projeto

Entregar um aplicativo multiplataforma (denominado **Cora**), composto por:

1. **Módulo administrativo para o lojista (web/mobile):** permite o controle ágil de estoque, registro de transações e visualização de métricas básicas de vendas.
2. **Módulo de interação com o cliente (mobile):** possibilita a consulta ao catálogo de produtos, acompanhamento de programas de fidelidade e envio de avaliações diretamente ao estabelecimento.

---

## 7. Objetivos Específicos do Projeto

* **7.1.** **Levantar** os requisitos funcionais e não funcionais junto a comerciantes locais para entender as maiores dores na gestão cotidiana.
* **7.2.** **Documentar** o fluxo de dados, casos de uso e a arquitetura de banco de dados do sistema por meio de diagramas UML.
* **7.3.** **Projetar** as interfaces de usuário (focando em acessibilidade e facilidade de uso) usando ferramentas de prototipação.
* **7.4.** **Desenvolver** a API (backend) de gerenciamento e armazenamento de dados.
* **7.5.** **Construir** o aplicativo multiplataforma (frontend) integrando o painel do comerciante e a área de feedback do cliente.
* **7.6.** **Testar** as funcionalidades da aplicação sob cenários reais de uso de venda e interação de estoque.
* **7.7.** **Avaliar** a satisfação e a usabilidade do protótipo final diretamente com lojistas e consumidores parceiros da FATEC Franca.

---

## 8. Metodologia de Execução do Projeto

| Item | Atividade | Ferramentas & Métodos Utilizados |
| :--- | :--- | :--- |
| **8.1** | **Levantamento de Requisitos (7.1)** | Entrevistas semiestruturadas, questionários online e aplicação do método ágil de **Design Thinking** (etapa de empatia). |
| **8.2** | **Documentação (7.2)** | Modelagem orientada a objetos (diagramas de classe, caso de uso e DER) utilizando **draw.io** ou **Astah**. |
| **8.3** | **Projetação de Interface (7.3)** | Prototipação de baixa e alta fidelidade via **Figma**, aplicando as heurísticas de Nielsen e design centrado no usuário. |
| **8.4** | **Desenvolvimento de Backend (7.4)** | Ambiente **Node.js** com framework **Express**, banco de dados relacional **PostgreSQL** e infraestrutura em nuvem. |
| **8.5** | **Construção de Frontend (7.5)** | Framework multiplataforma **Flutter** (Linguagem Dart) ou **React Native** para build nativo em Android e iOS. |
| **8.6** | **Testes (7.6)** | Testes de unidade e integração nos endpoints da API, além de testes de usabilidade "caixa preta" na interface do usuário. |
| **8.7** | **Avaliação (7.7)** | Aplicação do método de escala de satisfação **SUS (System Usability Scale)** e coleta de feedback qualitativo. |
