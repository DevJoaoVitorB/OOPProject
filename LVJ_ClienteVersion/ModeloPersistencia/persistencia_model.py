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
        objeto_id = getattr(objeto, 'id', None)
        if objeto_id is not None:
            novo_id = max([getattr(o, 'id', 0) for o in self.objetos], default=0) + 1
            setattr(objeto, 'id', novo_id)
        self.objetos.append(objeto)
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

    def listar(self) -> List[T]:
        self.abrir()
        return self.objetos

    def listar_id(self, id: int) -> Optional[T]:
        self.abrir()
        return next((o for o in self.objetos if getattr(o, 'id', None) == id), None)

    def abrir(self):
        self.objetos.clear()
        try:
            with open(self.filename, 'r', encoding='utf-8') as f:
                self.objetos = json.load(f)
        except (FileNotFoundError, json.JSONDecodeError):
            self.objetos = []

    def salvar(self):
        with open(self.filename, 'w', encoding='utf-8') as f:
            json.dump(self.objetos, f, default=lambda o: o.__dict__, indent=4)

# Classes que herdam de Persistencia

class Usuarios(Persistencia):
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
