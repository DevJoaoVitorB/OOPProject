# using System.Text.Json;
import json
import os
from abc import ABC, abstractmethod
from typing import List, TypeVar

T = TypeVar('T')  # Define um tipo genérico T

class Persistencia(ABC):
    def __init__(self):
        self.objetos: List[T] = []

    def inserir(self, objeto: T):
        # Abrir o arquivo .json com objetos da lista de objetos
        self.abrir()
        # Obter o maior ID da lista de objetos e somar +1
        id = 0 if not self.objetos else max(getattr(x, 'id') for x in self.objetos)
        setattr(objeto, 'id', id + 1)  # Define o novo ID
        # Adicionar o objeto na lista de objetos e salvar
        self.objetos.append(objeto)
        self.salvar()

    def remover(self, objeto: T):
        # Remover o objeto da lista de objetos e salvar
        if objeto is not None:
            self.objetos.remove(objeto)
            self.salvar()

    def atualizar(self, objeto: T):
        # Atualizar as propriedades do objeto da lista de objetos e salvar
        id = getattr(objeto, 'id')
        x = self.listar_id(id)

        if x is not None:
            index = self.objetos.index(x)
            self.objetos[index] = objeto
            self.salvar()

    def listar(self) -> List[T]:
        # Retorna a lista de objetos
        self.abrir()
        return self.objetos

    def listar_id(self, id: int) -> T:
        # Retornar o objeto que possui o ID informado
        self.abrir()
        return next((x for x in self.objetos if getattr(x, 'id') == id), None)

    @abstractmethod
    def abrir(self):
        pass

    @abstractmethod
    def salvar(self):
        pass
