package br.com.aquasos.controller;

import br.com.aquasos.model.Usuario;
import br.com.aquasos.model.dto.UsuarioDTO;
import br.com.aquasos.repository.UsuarioRepository;
import jakarta.validation.Valid;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.cache.annotation.CacheEvict;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@RestController
@RequestMapping("/usuarios")
public class UsuarioController {

    private final Logger log = LoggerFactory.getLogger(getClass());

    @Autowired
    private UsuarioRepository repository;

    @GetMapping
    @Cacheable("usuarios")
    public List<UsuarioDTO> index() {
        log.info("Listando usuários");
        return repository.findAll().stream()
                .map(u -> new UsuarioDTO(
                        u.getId(),
                        u.getNome(),
                        u.getEmail(),
                        u.getTelefone(),
                        u.getTipoUsuario()))
                .toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @CacheEvict(value = "usuarios", allEntries = true)
    public UsuarioDTO create(@RequestBody @Valid UsuarioDTO dto) {
        log.info("Cadastrando usuário {}", dto.nome());
        Usuario usuario = new Usuario();
        usuario.setNome(dto.nome());
        usuario.setEmail(dto.email());
        usuario.setTelefone(dto.telefone());
        usuario.setTipoUsuario(dto.tipoUsuario());
        Usuario saved = repository.save(usuario);
        return new UsuarioDTO(
                saved.getId(),
                saved.getNome(),
                saved.getEmail(),
                saved.getTelefone(),
                saved.getTipoUsuario());
    }

    @GetMapping("{id}")
    public UsuarioDTO get(@PathVariable Long id) {
        log.info("Buscando usuário {}", id);
        Usuario usuario = getUsuario(id);
        return new UsuarioDTO(
                usuario.getId(),
                usuario.getNome(),
                usuario.getEmail(),
                usuario.getTelefone(),
                usuario.getTipoUsuario());
    }

    @DeleteMapping("{id}")
    @CacheEvict(value = "usuarios", allEntries = true)
    public ResponseEntity<String> destroy(@PathVariable Long id) {
        log.info("Apagando usuário {}", id);
        Usuario usuario = getUsuario(id);
        repository.delete(usuario);
        log.info("Usuário {} apagado com sucesso", id);
        return ResponseEntity.ok("Usuário com ID " + id + " foi deletado com sucesso.");
    }

    @PutMapping("{id}")
    @CacheEvict(value = "usuarios", allEntries = true)
    public UsuarioDTO update(@PathVariable Long id, @RequestBody @Valid UsuarioDTO dto) {
        log.info("Atualizando usuário {} para {}", id, dto.nome());
        Usuario usuario = getUsuario(id);
        usuario.setNome(dto.nome());
        usuario.setEmail(dto.email());
        usuario.setTelefone(dto.telefone());
        usuario.setTipoUsuario(dto.tipoUsuario());
        Usuario updated = repository.save(usuario);
        return new UsuarioDTO(
                updated.getId(),
                updated.getNome(),
                updated.getEmail(),
                updated.getTelefone(),
                updated.getTipoUsuario());
    }

    private Usuario getUsuario(Long id) {
        return repository.findById(id)
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Usuário não encontrado"));
    }
}