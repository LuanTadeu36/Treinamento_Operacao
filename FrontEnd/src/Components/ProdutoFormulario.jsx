import React from 'react';
import { Box, TextField, Button } from '@mui/material';

export default function ProdutoFormulario({
  editValues,
  handleChange,
  handleSaveClick,
  handleCancelForm,
  editId,
}) {
  const { nome, quantidade, preco, nomeFornecedor, numFornecedor } = editValues;

  return (
    <Box
      component="form"
      sx={{
        '& > :not(style)': { m: 1, width: '25ch' },
      }}
      noValidate
      autoComplete="off"
      style={{ marginTop: '20px' }}
    >
      <TextField
        label="Nome"
        name="nome"
        value={nome || ''} 
        onChange={handleChange}
      />
      <TextField
        label="Quantidade"
        name="quantidade"
        value={quantidade || ''} 
        onChange={handleChange}
        type="number"
      />
      <TextField
        label="Preço"
        name="preco"
        value={preco || ''} 
        onChange={handleChange}
      />
      <TextField
        label="Nome do Fornecedor"
        name="nomeFornecedor"
        value={nomeFornecedor || ''}
        onChange={handleChange}
      />
      <TextField
        label="Número do Fornecedor"
        name="numFornecedor"
        value={numFornecedor || ''} 
        onChange={handleChange}
      />
      <Button variant="contained" color="primary" onClick={handleSaveClick}>
        {editId === null ? 'Adicionar Novo' : 'Salvar Alterações'}
      </Button>
      <Button variant="contained" color="secondary" onClick={handleCancelForm}>
        Cancelar
      </Button>
    </Box>
  );
}
