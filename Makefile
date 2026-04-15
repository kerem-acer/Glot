SLN = Glot.slnx
BENCH_PROJ = benchmarks/Glot.Benchmarks.csproj
BENCH_RUN = dotnet run --project $(BENCH_PROJ) -c Release --

.PHONY: build test coverage bench quick dry list clean restore

# --- Build & Test ---

build: ## Build solution
	dotnet build $(SLN) -c Release

test: ## Run all tests
	dotnet test --solution $(SLN) -c Release

coverage: ## Run tests with coverage report
	dotnet test --solution $(SLN) -c Release -- \
		--coverage --coverage-settings coverage.settings.xml \
		--coverage-output-format cobertura \
		--coverage-output coverage.cobertura.xml
	reportgenerator \
		-reports:"tests/*/bin/Release/net10.0/TestResults/coverage.cobertura.xml" \
		-targetdir:TestResults/CoverageReport \
		-reporttypes:"TextSummary"

restore: ## Restore packages
	dotnet restore $(SLN)

clean: ## Clean build outputs
	dotnet clean $(SLN) -c Release

# --- Benchmarks ---

bench: ## make bench F=ContainsUtf8 P='N=256 Locale=Ascii'
	$(BENCH_RUN) $(if $(F),--filter '*$(F)*') $(foreach p,$(P),--param:$(p)) $(ARGS)

quick: ## make quick F=ContainsUtf8 P='N=256 Locale=Ascii'
	$(BENCH_RUN) $(if $(F),--filter '*$(F)*') $(foreach p,$(P),--param:$(p)) --quick $(ARGS)

dry: ## make dry F=ContainsUtf8 P='N=256 Locale=Ascii'
	$(BENCH_RUN) $(if $(F),--filter '*$(F)*') $(foreach p,$(P),--param:$(p)) --job dry $(ARGS)

list: ## make list F=ContainsUtf8
	$(BENCH_RUN) --list flat $(if $(F),--filter '*$(F)*')
