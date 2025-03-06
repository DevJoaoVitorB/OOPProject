from Views import View
from Template.mantercarrinhoUI import ManterCarrinho

class UI:
    # dados do usuário logado
    cliente_id = 0
    cliente_nome = ""
    manter_carrinho = None
    id_venda_atual = None
    
    @staticmethod
    def menu_visitante():
        print("1 - Abrir conta, 2 - Entrar no Sistema, 99 - Fim")
        op = int(input("\nInforme uma opção:"))
        if op == 1: UI.visitante_abrir_conta()
        if op == 2: UI.visitante_entrar_no_sistema()
        return op

    def menu_cliente():
        print("1 - Listar produtos do catálogo (OK), 2 - Listar produtos do carrinho (OK), 3 - Adicionar produto no carrinho (OK), 4 - Atualizar produto do carrinho (OK), 5 - Remover produto do carrinho (OK), 6 - Realizar nova compra\n")
        print("7 - Fechar compra, 8 - Ver meus pedidos, 9 - Resgatar meu produto digital, - SAIR, 99 - FIM\n")
        print("99 - Fim\n")

        # 1 - Listar produtos do catálogo (OK), 2 - listar produtos do carrinho (OK), 3 - adicionar produto no carrinho (OK), 4 - atualizar produto do carrinho (+/-), 5 - remover produto do carrinho, 6 - realizar nova compra 
        # 7 - fechar compra, 8 - ver meus pedidos, 9 - Resgatar meu produto digital, - SAIR, 99 - FIM

        opcao = int(input("\nInforme uma opção:"))
        if opcao == 0:
            # CONCLUIDA
            UI.sair_do_sistema()
        if opcao == 1:
            # CONCLUIDA
            UI.produto_listar()
        if opcao == 2:
            # CONCLUIDA
            UI.cliente_listar_produto()
        if opcao == 3:
            # CONCLUIDA
            UI.produto_listar()
            UI.cliente_adicionar_produto()
        if opcao == 4:
            # SEMI
            UI.cliente_listar_produto()
            UI.cliente_atualizar_produto()
        if opcao == 5:
            UI.cliente_listar_produto()
            UI.cliente_remover_produto_do_carrinho()
        if opcao == 6:
            # UI.cliente_limpar_carrinho()
            pass
        if opcao == 7:
            # UI.cliente_fechar_compra()
            pass
        if opcao == 8:
            UI.cliente_meus_pedidos()
        if opcao == 9:
            UI.cliente_resgatar_produtos_digitais()
        if opcao == 99:
            UI.sair_do_sistema()

        return opcao

    @classmethod
    def main(cls):
        op = 0
        while op != 99:
            if cls.cliente_id == 0:
                # usuário não está logado
                op = UI.menu_visitante()                 
            else:
                # usuário está logado, verifica se é o admin
                admin = cls.cliente_nome == "admin"
                # mensagen de bem-vindo
                print("Bem-vindo(a), " + cls.cliente_nome)
                print("\n")
                # menu do usuário
                if admin: op = UI.menu_admin()
                else: op = UI.menu_cliente()

    @classmethod 
    def visitante_abrir_conta(cls):
        cls.cliente_inserir()

    @classmethod    
    def visitante_entrar_no_sistema(cls):
        email = input("Informe o email: ")
        senha = input("Informe a senha: ")
        view = View()  
        obj = view.cliente_autenticar(email, senha)  
        
        if obj is None:
            print("E-mail ou senha inválidos")
        else:
            cls.cliente_id = obj["id"]  
            cls.cliente_nome = obj["nome"]
            cls.manter_carrinho = ManterCarrinho(cls.cliente_id)

    @classmethod
    def sair_do_sistema(cls):
            cls.cliente_id = 0
            cls.cliente_nome = ""

    @classmethod 
    def cliente_inserir(cls):
        nome = input("Informe o nome:")
        email = input("Informe o email:")
        senha = input("Informe a senha:")
        endereco = input("Informe o endereco:")
        cep = input("Informe o CEP:")
        cpf = input("Informe o CPF:")
        admin = False
         
        view = View()
        view = view.cliente_inserir(nome, email, senha, endereco, cep, cpf, admin)
    
    @classmethod 
    def produto_listar(cls):
        view = View()
        objetos = view.produto_listar()

        if len(objetos) == 0:
            print("Nenhum produto cadastrado")
        else:
            print("PRODUTOS DA LOJA DE JOGOS VIRTUAL:")
            for obj in objetos:
                id_categoria = obj["id"]  

                categorias = view.categoria_listar()
                categoria = view.categoria_listar_id(id_categoria)
                if categoria is None:
                    print(f"{obj["descricao"]} - Categoria não encontrada")
                else:
                    print(f"{obj["id"]} - {obj["descricao"]} - {categoria["descricao"]}")  # Ajustado para acessar corretamente

    @classmethod
    def cliente_listar_produto(cls):
        view = View()
        objetos = view.carrinho_listar()

        if not objetos:
            print("O carrinho está vazio!")
        else:
            print("\nPRODUTOS NO CARRINHO:")
            for obj in objetos:
                print(f"\nID do Produto: {obj.get('idProduto', 'N/A')} // Quantidade: {obj.get('quantidade', 0)} // Preço Unitário: R$ {obj.get('precoProduto', 0.0):.2f} //\n Preço Total (Quantidade x Preço): R$ {obj.get('precoTotal', 0.0):.2f} // ID da Venda: {obj.get('idVenda', 'N/A')}\n")
                
    @classmethod
    def cliente_adicionar_produto(cls):
        print("\nADICIONANDO PRODUTOS AO CARRINHO:\n")

        view = View()
        objetos = view.produto_listar()

        if len(objetos) == 0:
            print("Nenhum produto cadastrado")
        else:
            id_produto = int(input("ID do produto:"))
            quantidade = int(input("Quantidade:"))

            for obj in objetos:
                if obj["id"] == id_produto:
                    preco_produto = obj["preco"]
                    estoque_produto = obj["estoque"]
                    digital_produto = obj["digital"]
                    id_categoria_produto = obj["idCategoria"]
                    codigo = ""

            if digital_produto:
                escolha = int(input("Você deseja receber o produto de forma digital ou física? (0 - Digital / 1 - Física):"))
                if escolha == 1:
                    digital_produto = False
                elif escolha == 0:
                    codigo = input("Digite uma senha para receber o seu produto virtual após finalizar a compra: ")

            enviado = False
            recebido = False

            # 🔹 Buscando o ID da última venda para definir um novo
            id_venda_atual = view.venda.ultimo_id_venda() + 1  # Criar um método na persistência para pegar o último ID de venda
            
            print(f"ID do Cliente: {cls.cliente_id} | ID da Venda: {id_venda_atual}")

            # 🔹 Envia para a View (que enviará para a persistência)
            view.produto_inserir_venda(
                cls.cliente_id, quantidade, preco_produto, digital_produto, 
                codigo, enviado, recebido, id_produto, id_venda_atual
            )

            print("Produto adicionado ao carrinho com sucesso!")

    @classmethod
    def cliente_atualizar_produto(cls):
        print("\nEXCLUINDO PRODUTOS DO CARRINHO:\n")

        view = View()
        objetos = view.carrinho_listar()
        id_venda_atual = view.id_venda_atual()

        if not objetos:
            print("Nenhum produto cadastrado.")
            return  # Sai do método se o carrinho estiver vazio

        id_produto = int(input("ID do produto a ser excluído: "))
        quantidade_para_remover = int(input("Quantidade a ser removida: "))

        cliente_id = None
        produto_selecionado = None

        for obj in objetos:
            if obj["idProduto"] == id_produto and obj["idVenda"] == id_venda_atual:
                cliente_id = obj["id"]
                produto_selecionado = obj
                break  # Encontrou o produto, para o loop

        if produto_selecionado is None:
            print("Erro: Produto não encontrado no carrinho!")
            return  # Sai do método se o produto não for encontrado

        # Extraindo os valores corretos do JSON
        quantidade_atual = produto_selecionado["quantidade"]
        preco_produto = produto_selecionado["precoProduto"]
        compra_valor_total = produto_selecionado["precoTotal"]
        resgate = produto_selecionado["resgate"]
        codigo = produto_selecionado["codigo"]
        enviado = produto_selecionado["enviado"]
        recebido = produto_selecionado["recebido"]
        idVenda = produto_selecionado["idVenda"]

        # Verificando se a quantidade a ser removida é válida
        if quantidade_para_remover > quantidade_atual:
            print("ERRO: Quantidade a ser removida é maior do que a disponível no carrinho.")
            return

        # Atualiza o ID da venda
        id_venda_atual = view.venda.carregar_id_venda_atual()

        print(f"ID do Cliente: {cliente_id} | ID da Venda: {id_venda_atual}")

        # Envia para a View (que enviará para a persistência)
        view.produtovenda_excluir(
            cliente_id, quantidade_para_remover, quantidade_atual,
            preco_produto, compra_valor_total, resgate, codigo,
            enviado, recebido, id_produto, id_venda_atual
        )

        print("Produto removido do carrinho com sucesso!")


    @classmethod
    def cliente_remover_produto_do_carrinho(cls):
        print("\REMOVENDO PRODUTO DO CARRINHO:\n")

        view = View()
        objetos = view.carrinho_listar()
        id_venda_atual = view.id_venda_atual()

        if not objetos:
            print("Nenhum produto cadastrado.")
            return  # Sai do método se o carrinho estiver vazio

        id_produto = int(input("ID do produto a ser excluído: "))

        cliente_id = None
        produto_selecionado = None

        for obj in objetos:
            if obj["idProduto"] == id_produto and obj["idVenda"] == id_venda_atual:
                quantidade_para_remover = obj["quantidade"]
                cliente_id = obj["id"]
                produto_selecionado = obj
                break  # Encontrou o produto, para o loop

        if produto_selecionado is None:
            print("Erro: Produto não encontrado no carrinho!")
            return  # Sai do método se o produto não for encontrado

        # Extraindo os valores corretos do JSON
        quantidade_atual = produto_selecionado["quantidade"]
        preco_produto = produto_selecionado["precoProduto"]
        compra_valor_total = produto_selecionado["precoTotal"]
        resgate = produto_selecionado["resgate"]
        codigo = produto_selecionado["codigo"]
        enviado = produto_selecionado["enviado"]
        recebido = produto_selecionado["recebido"]
        idVenda = produto_selecionado["idVenda"]

        # Atualiza o ID da venda
        id_venda_atual = view.venda.carregar_id_venda_atual()

        print(f"ID do Cliente: {cliente_id} | ID da Venda: {id_venda_atual}")

        # Envia para a View (que enviará para a persistência)
        view.produtovenda_excluir(
            cliente_id, quantidade_para_remover, quantidade_atual,
            preco_produto, compra_valor_total, resgate, codigo,
            enviado, recebido, id_produto, id_venda_atual
        )

        print("Produto removido do carrinho com sucesso!")
        
    
    @classmethod
    def cliente_meus_pedidos(cls):
        print("\nMEUS PEDIDOS:\n")
        #em breve será adiciona
    
    
    @classmethod
    def cliente_fechar_compra(cls):
        print("FECHANDO COMPRA:")
        dia = input("Digite a data de hoje:")
        parcela = input("Digite o número de parcelas: (Digite 1 caso o pagamento seja a vista)")
        idFormaPagamento = input()
    @classmethod
    def cliente_resgatar_produtos_digitais(cls):
        print("\nResgate Seu Jogo Digital Aqui!")
        codigo = ''
        
        while True:
            codigo = input("Digite o código de acesso do seu jogo:")
            if codigo == "senha":
                print("Jogo Retirado Com Sucesso!")
                break
            else:
                print("Código inválido!")

UI.main()