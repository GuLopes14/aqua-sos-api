package br.com.aquasos.controller;

import br.com.aquasos.model.PontoDistribuicao;
import br.com.aquasos.model.dto.PontoDistribuicaoDTO;
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
@RequestMapping("/pontos-distribuicao")
public class PontoDistribuicaoController {

    private final Logger log = LoggerFactory.getLogger(getClass());

    @Autowired
    private PontoDistribuicaoRepository repository;

    @GetMapping
    @Cacheable("pontosDistribuicao")
    public List<PontoDistribuicaoDTO> index() {
        log.info("Listando pontos de distribuição");
        return repository.findAll().stream()
                .map(p -> new PontoDistribuicaoDTO(
                        p.getId(),
                        p.getNome(),
                        p.getEndereco(),
                        p.getCidade(),
                        p.getCapacidadeTotalLitros()))
                .toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @CacheEvict(value = "pontosDistribuicao", allEntries = true)
    public PontoDistribuicaoDTO create(@RequestBody @Valid PontoDistribuicaoDTO dto) {
        log.info("Cadastrando ponto de distribuição {}", dto.nome());
        PontoDistribuicao ponto = new PontoDistribuicao();
        ponto.setNome(dto.nome());
        ponto.setEndereco(dto.endereco());
        ponto.setCidade(dto.cidade());
        ponto.setCapacidadeTotalLitros(dto.capacidadeTotalLitros());
        PontoDistribuicao saved = repository.save(ponto);
        return new PontoDistribuicaoDTO(
                saved.getId(),
                saved.getNome(),
                saved.getEndereco(),
                saved.getCidade(),
                saved.getCapacidadeTotalLitros());
    }

    @GetMapping("{id}")
    public PontoDistribuicaoDTO get(@PathVariable Long id) {
        log.info("Buscando ponto de distribuição {}", id);
        PontoDistribuicao ponto = getPonto(id);
        return new PontoDistribuicaoDTO(
                ponto.getId(),
                ponto.getNome(),
                ponto.getEndereco(),
                ponto.getCidade(),
                ponto.getCapacidadeTotalLitros());
    }

    @DeleteMapping("{id}")
    @CacheEvict(value = "pontosDistribuicao", allEntries = true)
    public ResponseEntity<String> destroy(@PathVariable Long id) {
        log.info("Apagando ponto de distribuição {}", id);
        PontoDistribuicao ponto = getPonto(id);
        repository.delete(ponto);
        log.info("Ponto de distribuição {} apagado com sucesso", id);
        return ResponseEntity.ok("Ponto de distribuição com ID " + id + " foi deletado com sucesso.");
    }

    @PutMapping("{id}")
    @CacheEvict(value = "pontosDistribuicao", allEntries = true)
    public PontoDistribuicaoDTO update(@PathVariable Long id, @RequestBody @Valid PontoDistribuicaoDTO dto) {
        log.info("Atualizando ponto de distribuição {} para {}", id, dto.nome());
        PontoDistribuicao ponto = getPonto(id);
        ponto.setNome(dto.nome());
        ponto.setEndereco(dto.endereco());
        ponto.setCidade(dto.cidade());
        ponto.setCapacidadeTotalLitros(dto.capacidadeTotalLitros());
        PontoDistribuicao updated = repository.save(ponto);
        return new PontoDistribuicaoDTO(
                updated.getId(),
                updated.getNome(),
                updated.getEndereco(),
                updated.getCidade(),
                updated.getCapacidadeTotalLitros());
    }

    private PontoDistribuicao getPonto(Long id) {
        return repository.findById(id)
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Ponto de distribuição não encontrado"));
    }
}