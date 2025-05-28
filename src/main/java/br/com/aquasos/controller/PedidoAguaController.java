package br.com.aquasos.controller;

import br.com.aquasos.model.PedidoAgua;
import br.com.aquasos.model.Usuario;
import br.com.aquasos.model.PontoDistribuicao;
import br.com.aquasos.model.dto.PedidoAguaDTO;
import br.com.aquasos.repository.PedidoAguaRepository;
import br.com.aquasos.repository.UsuarioRepository;
import br.com.aquasos.repository.PontoDistribuicaoRepository;
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
@RequestMapping("/pedidos-agua")
public class PedidoAguaController {

    private final Logger log = LoggerFactory.getLogger(getClass());

    @Autowired
    private PedidoAguaRepository repository;

    @Autowired
    private UsuarioRepository usuarioRepository;

    @Autowired
    private PontoDistribuicaoRepository pontoDistribuicaoRepository;

    @GetMapping
    @Cacheable("pedidosAgua")
    public List<PedidoAguaDTO> index() {
        log.info("Listando pedidos de água");
        return repository.findAll().stream()
                .map(p -> new PedidoAguaDTO(
                        p.getId(),
                        p.getUsuario().getId(),
                        p.getPonto().getId(),
                        p.getQuantidadeLitros(),
                        p.getDataSolicitacao(),
                        p.getStatus()))
                .toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @CacheEvict(value = "pedidosAgua", allEntries = true)
    public PedidoAguaDTO create(@RequestBody @Valid PedidoAguaDTO dto) {
        log.info("Cadastrando pedido de água para usuário {}", dto.usuarioId());
        Usuario usuario = usuarioRepository.findById(dto.usuarioId())
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.BAD_REQUEST, "Usuário não encontrado"));
        PontoDistribuicao ponto = pontoDistribuicaoRepository.findById(dto.pontoDistribuicaoId())
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.BAD_REQUEST, "Ponto de distribuição não encontrado"));

        PedidoAgua pedido = new PedidoAgua();
        pedido.setUsuario(usuario);
        pedido.setPonto(ponto);
        pedido.setQuantidadeLitros(dto.quantidadeLitros());
        pedido.setDataSolicitacao(dto.dataSolicitacao());
        pedido.setStatus(dto.status());

        PedidoAgua saved = repository.save(pedido);

        return new PedidoAguaDTO(
                saved.getId(),
                saved.getUsuario().getId(),
                saved.getPonto().getId(),
                saved.getQuantidadeLitros(),
                saved.getDataSolicitacao(),
                saved.getStatus());
    }

    @GetMapping("{id}")
    public PedidoAguaDTO get(@PathVariable Long id) {
        log.info("Buscando pedido de água {}", id);
        PedidoAgua pedido = getPedido(id);
        return new PedidoAguaDTO(
                pedido.getId(),
                pedido.getUsuario().getId(),
                pedido.getPonto().getId(),
                pedido.getQuantidadeLitros(),
                pedido.getDataSolicitacao(),
                pedido.getStatus());
    }

    @DeleteMapping("{id}")
    @CacheEvict(value = "pedidosAgua", allEntries = true)
    public ResponseEntity<String> destroy(@PathVariable Long id) {
        log.info("Apagando pedido de água {}", id);
        PedidoAgua pedido = getPedido(id);
        repository.delete(pedido);
        log.info("Pedido de água {} apagado com sucesso", id);
        return ResponseEntity.ok("Pedido de água com ID " + id + " foi deletado com sucesso.");
    }

    @PutMapping("{id}")
    @CacheEvict(value = "pedidosAgua", allEntries = true)
    public PedidoAguaDTO update(@PathVariable Long id, @RequestBody @Valid PedidoAguaDTO dto) {
        log.info("Atualizando pedido de água {} para usuário {}", id, dto.usuarioId());
        PedidoAgua pedido = getPedido(id);

        Usuario usuario = usuarioRepository.findById(dto.usuarioId())
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.BAD_REQUEST, "Usuário não encontrado"));
        PontoDistribuicao ponto = pontoDistribuicaoRepository.findById(dto.pontoDistribuicaoId())
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.BAD_REQUEST, "Ponto de distribuição não encontrado"));

        pedido.setUsuario(usuario);
        pedido.setPonto(ponto);
        pedido.setQuantidadeLitros(dto.quantidadeLitros());
        pedido.setDataSolicitacao(dto.dataSolicitacao());
        pedido.setStatus(dto.status());

        PedidoAgua updated = repository.save(pedido);

        return new PedidoAguaDTO(
                updated.getId(),
                updated.getUsuario().getId(),
                updated.getPonto().getId(),
                updated.getQuantidadeLitros(),
                updated.getDataSolicitacao(),
                updated.getStatus());
    }

    private PedidoAgua getPedido(Long id) {
        return repository.findById(id)
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Pedido de água não encontrado"));
    }
}