import json
import os
from typing import List, TypeVar, Generic, Optional

T = TypeVar('T')

class Persistencia(Generic[T]):
    def __init__(self, filename: str):
        self.filename = filename
        self.objetos: List[T] = []
        self.abrir()

    def inserir(self, objeto: T):
        self.abrir()

        novo_id = max((obj.get('id', 0) for obj in self.objetos), default=0) + 1
        
        if not isinstance(objeto, dict):
            objeto_dict = {key.lstrip('_'): value for key, value in objeto.__dict__.items()}
        else:
            objeto_dict = objeto

        objeto_dict['id'] = novo_id  

        self.objetos.append(objeto_dict)
        self.salvar()
    def remover(self, objeto: T):
        self.objetos = [o for o in self.objetos if getattr(o, 'id', None) != getattr(objeto, 'id', None)]
        self.salvar()

    def atualizar(self, objeto: T):
        self.abrir()
        for i, o in enumerate(self.objetos):
            if getattr(o, 'id', None) == getattr(objeto, 'id', None):
                self.objetos[i] = objeto
                self.salvar()
                return

    def atualizar_carrinho(self, objeto: T):
        """
        Atualiza um produto no carrinho. Se a quantidade for 0, remove o item.
        """
        self.abrir()
        for i, o in enumerate(self.objetos):
            if isinstance(o, dict):  # Se for dicionário, comparar diretamente
                if o.get('id') == objeto.id and o.get('idProduto') == objeto.id_produto:
                    if objeto.quantidade > 0:
                        print(f"🔄 Atualizando produto ID {o['idProduto']} no carrinho...")
                        self.objetos[i] = objeto.to_json()  # Chama o método to_json
                    else:
                        print(f"🗑️ Removendo produto ID {o['idProduto']} do carrinho...")
                        self.objetos.pop(i)  # Remove produto se a quantidade for 0
                    self.salvar()
                    return
            elif getattr(o, 'id', None) == getattr(objeto, 'id', None) and getattr(o, 'idProduto', None) == getattr(objeto, 'idProduto', None):
                if objeto.quantidade > 0:
                    print(f"🔄 Atualizando produto ID {o.idProduto} no carrinho...")
                    self.objetos[i] = objeto.to_json()  # Chama o método to_json
                else:
                    print(f"🗑️ Removendo produto ID {o.idProduto} do carrinho...")
                    self.objetos.pop(i)
                self.salvar()
                return
        
        print("Produto não encontrado no carrinho.")

    def listar(self) -> List[T]:
        self.abrir()
        return self.objetos

    def listar_id(self, id: int) -> Optional[dict]:
        self.abrir()

        for obj in self.objetos:
            if obj.get("id") == id: 
                return obj

        return None

    def abrir(self):
        self.objetos.clear()
        try:
            with open(self.filename, 'r', encoding='utf-8') as f:
                self.objetos = json.load(f)
        except (FileNotFoundError, json.JSONDecodeError):
            self.objetos = []

    def salvar(self):
        with open(self.filename, "w") as arquivo:
            json.dump(self.objetos, arquivo, default=lambda o: o.to_json(), indent=4)

    def inserir_no_carrinho(self, objeto):
        self.abrir()

        id_venda_atual = self.carregar_id_venda_atual()

        if not isinstance(objeto, dict):
            objeto_dict = {key.lstrip('_'): value for key, value in objeto.to_json().items()}
        else:
            objeto_dict = objeto

        objeto_dict["idVenda"] = id_venda_atual

        self.objetos.append(objeto_dict)
        self.salvar()

    def salvar_id_venda_atual(self, id_venda):
        with open("id_venda_atual.json", "w") as f:
            json.dump({"idVendaAtual": id_venda}, f)

    def carregar_id_venda_atual(self):
        if os.path.exists("id_venda_atual.json"):
            with open("id_venda_atual.json", "r") as f:
                data = json.load(f)
                return data.get("idVendaAtual", 1)
        return 1

    def ultimo_id_venda(self):
        self.abrir()
        return max((obj.get('idVenda', 0) for obj in self.objetos), default=0)

# Classes que herdam de Persistencia
class Clientes(Persistencia):
    def __init__(self):
        super().__init__('Banco_Dados/lista_usuarios.json')

class Categorias(Persistencia):
    def __init__(self):
        super().__init__('Banco_Dados/lista_categorias.json')

class Produtos(Persistencia):
    def __init__(self):
        super().__init__('Banco_Dados/lista_produtos.json')

class ProdutoVendas(Persistencia):
    def __init__(self):
        super().__init__('Banco_Dados/lista_produtos_vendas.json')

class Vendas(Persistencia):
    def __init__(self):
        super().__init__('Banco_Dados/lista_vendas.json')

class FormaPagamentos(Persistencia):
    def __init__(self):
        super().__init__('Banco_Dados/lista_formas_pagamento.json')
