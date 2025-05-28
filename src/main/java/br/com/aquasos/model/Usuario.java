package br.com.aquasos.model;

import br.com.aquasos.model.enums.TipoUsuario;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import jakarta.validation.constraints.NotBlank;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Entity
@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class Usuario {
  @Id @GeneratedValue
  private Long id;
  @NotBlank
  private String nome;
  @Column(unique = true)
  private String email;
  private String telefone;
  @Enumerated(EnumType.STRING)
  @Column(nullable = false)
  private TipoUsuario tipoUsuario;
}

