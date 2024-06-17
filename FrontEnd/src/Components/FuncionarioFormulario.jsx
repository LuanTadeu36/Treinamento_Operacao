import React from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';

const FuncionarioFormulario = ({ editValues, handleChange, handleSaveClick, handleCancelForm, editId }) => {
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
        value={editValues.nome ||''}
        onChange={handleChange}
      />
      <TextField
        label="Cargo"
        name="cargo"
        value={editValues.cargo ||''}
        onChange={handleChange}
      />
      <TextField
        label="CPF"
        name="CPF"
        value={editValues.CPF ||''}
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

export default FuncionarioFormulario;
