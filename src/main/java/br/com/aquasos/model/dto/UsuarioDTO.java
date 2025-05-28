package br.com.aquasos.model.dto;

import br.com.aquasos.model.enums.TipoUsuario;

public record UsuarioDTO(
    Long id,
    String nome,
    String email,
    String password,
    TipoUsuario tipoUsuario
) {}
