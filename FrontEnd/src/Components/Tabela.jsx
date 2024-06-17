import React, { useState, useEffect, useRef } from 'react';
import { styled } from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import Paper from '@mui/material/Paper';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import Button from '@mui/material/Button';
import { TableRow, TextField } from '@mui/material';
import { listarTodos as listarTodosProduto, deletar as deletarProduto, atualizar as atualizarProduto, atualizarQuantidade } from './produtoServico';
import { listarTodos as listarTodosFuncionario, deletar as deletarFuncionario, atualizar as atualizarFuncionario } from './funcionarioServico';

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.common.black,
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  height: '50px',
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },
  '&:last-child td, &:last-child th': {
    border: 0,
  },
}));

const Tabela = ({ tipo, shouldFetch, setShouldFetch }) => {
  
  // Estado para armazenar os itens da tabela, o ID do item em edição, valores editados, etc.
  const [itens, setItens] = useState([]);
  const [editId, setEditId] = useState(null);
  const [editValues, setEditValues] = useState({});
  const [isEditing, setIsEditing] = useState(false);
  const [editingField, setEditingField] = useState(null);
  
  // Campos personalizados para cada tipo de tabela (produto ou funcionário)
  const camposPersonalizados = {
    produto: ['ID', 'Nome', 'Quantidade'],
    funcionario: ['ID', 'Nome', 'Cargo']
  };

  const fetchData = async () => {
      let dados;
      if (tipo === 'produto') {
        dados = await listarTodosProduto();
      } else if (tipo === 'funcionario') {
        dados = await listarTodosFuncionario();
      }
      setItens(dados);
    } 

  useEffect(() => {
    fetchData();
  }, [shouldFetch, tipo]);
  const tableRef = useRef(null);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (isEditing && tableRef.current && !tableRef.current.contains(event.target)) {
        handleCancelEditClick();
      }
    };

    document.addEventListener("mousedown", handleClickOutside);

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [isEditing]);

  const handleDeleteClick = async (id) => {
    
      if (tipo === 'produto') {
        await deletarProduto(id);
      } else if (tipo === 'funcionario') {
        await deletarFuncionario(id);
      }
      setShouldFetch(prev => !prev); 
    } 

  const handleEditClick = (item, campo) => {
    setEditId(item.id);
    setEditValues(item);
    setIsEditing(true);
    setEditingField(campo.toLowerCase());
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setEditValues(prev => ({ ...prev, [name]: value }));
  };

  const handleSaveClick = async () => {
    
      if (tipo === 'produto') {
        if (editingField === 'quantidade') {
          await atualizarQuantidade(editId, editValues);
        } else {
          await atualizarProduto(editId, editValues);
        }
      } else if (tipo === 'funcionario') {
        await atualizarFuncionario(editId, editValues);
      }
      setEditId(null);
      setEditValues({});
      setIsEditing(false);
      setEditingField(null);
      setShouldFetch(prev => !prev); 
  };

  const handleCancelEditClick = () => {
    setEditId(null);
    setEditValues({});
    setIsEditing(false);
    setEditingField(null);
  };

  const renderizarHead = () => (
    <TableRow>
      {camposPersonalizados[tipo].map((campo) => (
        <StyledTableCell key={campo}>{campo}</StyledTableCell>
      ))}
      <StyledTableCell>Ações</StyledTableCell>
    </TableRow>
  );

  const renderizarApi = () => (
    itens.map((item) => (
      <StyledTableRow key={item.id}>
        {camposPersonalizados[tipo].map((campo) => (
          <StyledTableCell key={campo}>
            {isEditing && editId === item.id && editingField === campo.toLowerCase() ? (
              <TextField
                variant="standard"
                name={campo.toLowerCase()}
                value={editValues[campo.toLowerCase()] || ''}
                onChange={handleInputChange}
                autoFocus
                type={campo.toLowerCase() === 'quantidade' ? 'number' : 'text'}
                InputProps={campo.toLowerCase() === 'quantidade' ? {
                  inputProps: { step: 1, min: 0 }
                } : {}}
              />
            ) : (
              <span
                style={{ cursor: 'pointer' }}
                onClick={() => {
                  if (campo.toLowerCase() !== 'id') {
                    handleEditClick(item, campo);
                  }
                }}
              >
                {item[campo.toLowerCase()]}
              </span>
            )}
          </StyledTableCell>
        ))}
        <StyledTableCell>
          {isEditing && editId === item.id ? (
            <>
              <Button
                variant="contained"
                color="primary"
                size="small"
                onClick={handleSaveClick}
                style={{ marginRight: '4px' }}
              >
                Salvar
              </Button>
              <Button
                variant="contained"
                color="secondary"
                size="small"
                onClick={handleCancelEditClick}
                style={{ marginRight: '4px' }}
              >
                Cancelar
              </Button>
            </>
          ) : (
            <>
              <Button
                variant="contained"
                color="primary"
                size="small"
                onClick={() => handleEditClick(item, 'Nome')} 
                style={{ marginRight: '4px' }}
              >
                Editar
              </Button>
              <IconButton
                aria-label="delete"
                size="small"
                onClick={() => handleDeleteClick(item.id)}
                style={{ marginLeft: '4px' }}
              >
                <DeleteIcon fontSize="small" />
              </IconButton>
            </>
          )}
        </StyledTableCell>
      </StyledTableRow>
    ))
  );

  const titulo = tipo === 'produto' ? 'Tabela de Produtos' : 'Tabela de Funcionários';

  return (
    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
      <h3>{titulo}</h3>
      <TableContainer
        ref={tableRef}
        component={Paper}
        style={{
          width: '80%',
          maxWidth: '1000px',
          maxHeight: '350px',
          overflowY: 'auto'
        }}
      >
        <Table sx={{ minWidth: 500 }} aria-label="customized table">
          <TableHead>{renderizarHead()}</TableHead>
          <TableBody>{renderizarApi()}</TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};

export default Tabela;
