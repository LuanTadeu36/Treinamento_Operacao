import axios from 'axios';

export function listarTodos() {
  return axios.get("https://localhost:7125/Funcionario")
    .then(response => response.data)
};

export function adicionar(funcionario) {
  return axios.post("https://localhost:7125/Funcionario", funcionario)
    .then(response => response.data)
};

export function atualizar(id,funcionario) {
  return axios.put("https://localhost:7125/Funcionario", funcionario)
    .then(response => response.data)
};

export function deletar(id) {
  return axios.delete(`https://localhost:7125/Funcionario/${id}`)
    .then(response => response.status === 200)
};
