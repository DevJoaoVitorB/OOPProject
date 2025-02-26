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
        print("1 - Listar Produtos Do Catálogo, 2 - Listar Produtos Do Carrinho, 3 - Adicionar Produto no Carrinho, 4 - Fechar Pedido, 5 - Ver Meus Pedidos, 6 - Atualizar Produto, 7 - Remover Produto\n")
        print("8 - Realizar Nova Compra, 9 - Fechar Minha Compra, 10 - Listar Minhas Compras, 11 - Listar Minhas Entregas, 12 - Resgatar Meu Produto Digital,\n")
        print("0 - Sair, 99 - Fim\n")
        opcao = int(input("\nInforme uma opção:"))
        if opcao == 0:
            UI.sair_do_sistema()
        if opcao == 1:
            UI.produto_listar()
        if opcao == 2:
            UI.cliente_listar_produto()
        if opcao == 3:
            UI.produto_listar()
            UI.cliente_adicionar_produto()
        if opcao == 4:
            # UI.cliente_fechar_pedido()
            pass
        if opcao == 5:
            UI.cliente_meus_pedidos()
        if opcao == 6:
            UI.cliente_listar_produto()
            UI.cliente_atualizar_produto()
        if opcao == 7:
            UI.cliente_remover_produto_do_carrinho()
        if opcao == 8:
            UI.cliente_realizar_compra()
        if opcao == 9:
            UI.cliente_fechar_compra()
        if opcao == 10:
            UI.cliente_listar_compras()
        if opcao == 11:
            UI.cliente_listar_entregas()
        if opcao == 12:
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
                print(f"\nID do Produto: {obj.get('id_produto', 'N/A')} // Quantidade: {obj.get('quantidade', 0)} // Preço Unitário: R$ {obj.get('preco_produto', 0.0):.2f} //\n Preço Total (Quantidade x Preço): R$ {obj.get('preco_total', 0.0):.2f} // ID da Venda: {obj.get('idVenda', 'N/A')}\n")
                
    @classmethod
    def cliente_adicionar_produto(cls):
        print("\nADICIONANDO PRODUTOS NO CARRINHO:\n")

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

    def cliente_atualizar_produto():

        print("\nEXCLUINDO PRODUTOS DO CARRINHO:\n")

        view = View()
        objetos = view.carrinho_listar()
        id_venda_atual = view.id_venda_atual()

        if len(objetos) == 0:
            print("Nenhum produto cadastrado")
        else:
            id_produto = int(input("ID do produto a ser excluído: "))
            quantidade_para_remover = int(input("Quantidade a ser removida: "))
            

            for obj in objetos:
                if obj["id_produto"] == id_produto and obj["idVenda"] == id_venda_atual:
                    cliente_id = obj["id"]
                    quantidade_atual = obj["quantidade"]
                    preco_produto = obj["preco_produto"]
                    compra_valor_total = obj["preco_total"]
                    resgate = obj["resgate"]
                    codigo = obj["codigo"]
                    enviado = obj["enviado"]
                    recebido = obj["recebido"]
                    id_produto = obj["id_produto"]
                    idVenda = obj["idVenda"]
            
            # 🔹 Buscando o ID da última venda para definir um novo
            id_venda_atual = view.venda.carregar_id_venda_atual()  # Criar um método na persistência para pegar o último ID de venda
            
            print(f"ID do Cliente: {obj["id"]} | ID da Venda: {id_venda_atual}")

            # 🔹 Envia para a View (que enviará para a persistência)
            view.produtovenda_excluir(cliente_id, quantidade_para_remover, quantidade_atual, preco_produto,compra_valor_total, resgate, codigo, enviado, recebido, id_produto, id_venda_atual)

            print("Produto removido do carrinho com sucesso!")

    @classmethod
    def cliente_remover_produto_do_carrinho(cls):
        print("\nRemovendo Produto do Carrinho\n")
    
    @classmethod
    def cliente_meus_pedidos(cls):
        print("\nMeus Pedidos:\n")
        #em breve será adiciona
    
    @classmethod
    def cliente_realizar_compra(cls):
        print("\nRealizando nova compra")
    
    @classmethod
    def cliente_fechar_compra(cls):
        print("\nCompra realizada com sucesso!")
    
    @classmethod
    def cliente_listar_compras(cls):
        print("\nMinhas Compras:")
    
    @classmethod
    def cliente_listar_entregas(cls):
        print("\nMinhas Entregas:")

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