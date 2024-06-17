import axios from 'axios';

export function listarTodos() {
  return axios.get("https://localhost:7125/Produto")
    .then(response => response.data)
};

export function adicionar(produto) {
  return axios.post("https://localhost:7125/Produto", produto)
    .then(response => response.data)
};

export function atualizar(id,produto) {
  return axios.put("https://localhost:7125/Produto", produto)
    .then(response => response.data)
};

export function atualizarQuantidade(id,produto) {
  return axios.patch(`https://localhost:7125/Produto?atributo=quantidade`, produto)
    .then(response => response.data)
};

export function deletar(id) {
  return axios.delete(`https://localhost:7125/Produto/${id}`)
    .then(response => response.status === 200)
};
