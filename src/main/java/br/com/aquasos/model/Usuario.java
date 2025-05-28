package br.com.aquasos.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
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
  private String nome;
  private String email;
  private String telefone;
  private String tipoUsuario; 
}

