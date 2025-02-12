# using System.Text.Json;
import json
import os
from abc import ABC, abstractmethod
from typing import List, TypeVar

T = TypeVar('T')  

class Persistencia(ABC):
    def __init__(self):
        self.objetos: List[T] = []

    def inserir(self, objeto: T):

        self.abrir()
        id = 0 if not self.objetos else max(getattr(x, 'id') for x in self.objetos)
        setattr(objeto, 'id', id + 1)  # Define o novo ID
        self.objetos.append(objeto)
        self.salvar()

    def remover(self, objeto: T):
        if objeto is not None:
            self.objetos.remove(objeto)
            self.salvar()

    def atualizar(self, objeto: T):
        id = getattr(objeto, 'id')
        x = self.listar_id(id)

        if x is not None:
            index = self.objetos.index(x)
            self.objetos[index] = objeto
            self.salvar()

    def listar(self) -> List[T]:
        self.abrir()
        return self.objetos

    def listar_id(self, id: int) -> T:
        self.abrir()
        return next((x for x in self.objetos if getattr(x, 'id') == id), None)

    @abstractmethod
    def abrir(self):
        pass

    @abstractmethod
    def salvar(self):
        pass
