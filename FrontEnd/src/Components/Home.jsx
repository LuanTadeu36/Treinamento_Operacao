import React, { useState, useEffect } from 'react';
import Tabela from './Tabela';
import ProdutoFormulario from './ProdutoFormulario';
import FuncionarioFormulario from './FuncionarioFormulario';
import Button from '@mui/material/Button';
import { adicionar as adicionarProduto, atualizar as atualizarProduto, listarTodos as listarTodosProduto } from './produtoServico';
import { adicionar as adicionarFuncionario, atualizar as atualizarFuncionario, listarTodos as listarTodosFuncionario } from './funcionarioServico';

const Home = () => {
  const [currentPage, setCurrentPage] = useState('produto');
  const [showForm, setShowForm] = useState(false);
  const [editValues, setEditValues] = useState({});
  const [editId, setEditId] = useState(null);
  const [shouldFetch, setShouldFetch] = useState(false); 
  const [ultimoIdProduto, setUltimoIdProduto] = useState(0);
  const [ultimoIdFuncionario, setUltimoIdFuncionario] = useState(0);

  useEffect(() => {
    const fetchData = async () => {
      
        const produtos = await listarTodosProduto();
        if (produtos.length > 0) {
          const ultimoId = produtos[produtos.length - 1].id;
          setUltimoIdProduto(ultimoId);
        } else {
          setUltimoIdProduto(0);
        }

        const funcionarios = await listarTodosFuncionario();
        if (funcionarios.length > 0) {
          const ultimoId = funcionarios[funcionarios.length - 1].id;
          setUltimoIdFuncionario(ultimoId);
        } else {
          setUltimoIdFuncionario(0);
        }
    };

    fetchData();
  }, [shouldFetch]);

  const handleButtonClick = (page) => {
    setCurrentPage(page);
    setShowForm(false);
    setEditValues({});
    setEditId(null);
  };

  const handleShowForm = () => {
    setShowForm(true);
    setEditValues({});
    setEditId(null);
  };

  const handleHideForm = () => {
    setShowForm(false);
  };

  const handleSaveClick = async () => {
    
      if (currentPage === 'produto') {
        if (editId === null) {
          const novoProduto = { ...editValues, id: ultimoIdProduto + 1 };
          await adicionarProduto(novoProduto);
          setUltimoIdProduto(ultimoIdProduto + 1);
        } else {
          await atualizarProduto(editId, editValues);
        }
      } else {
        if (editId === null) {
          const novoFuncionario = { ...editValues, id: ultimoIdFuncionario + 1 };
          await adicionarFuncionario(novoFuncionario);
          setUltimoIdFuncionario(ultimoIdFuncionario + 1);
        } else {
          await atualizarFuncionario(editId, editValues);
        }
      }
      setShouldFetch(prev => !prev);
      handleHideForm();
    
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEditValues(prev => ({ ...prev, [name]: value }));
  };

  return (
    <div>
      <h1>Treinamento Front</h1>
      <div style={{ display: 'flex', justifyContent: 'flex-end', marginBottom: '10px' }}>
        <Button
          variant="contained"
          onClick={() => handleButtonClick(currentPage === 'produto' ? 'funcionario' : 'produto')}
          style={{ marginLeft: '10px' }}
        >
          {currentPage === 'produto' ? 'Funcionário' : 'Produto'}
        </Button>
      </div>
      <Tabela tipo={currentPage} shouldFetch={shouldFetch} setShouldFetch={setShouldFetch} />
      <br /><br />
      <Button variant="contained" color="primary" onClick={handleShowForm}>
        Adicionar Novo
      </Button>
      {showForm && (
        currentPage === 'produto' ? (
          <ProdutoFormulario
            editValues={editValues}
            handleChange={handleChange}
            handleSaveClick={handleSaveClick}
            handleCancelForm={handleHideForm}
            editId={editId}
          />
        ) : (
          <FuncionarioFormulario
            editValues={editValues}
            handleChange={handleChange}
            handleSaveClick={handleSaveClick}
            handleCancelForm={handleHideForm}
            editId={editId}
          />
        )
      )}
    </div>
  );
};

export default Home;
