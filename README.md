# tax.crypto.com Transaction Converter
Basic application to take various exchange CSV transaction files and convert to the generic CSV format for tax.crypto.com

## Build and Test

Build
```shell
dotnet build CryptoComTax.sln
```

Test
```shell
dotnet test CryptoComTax.sln
```

## Usage

Run Command-line shell utility with arguments to control the application:
- `-e`, ` --exchange` - *Required*. Which exchange file to convert from. Allowed values (not case-sensitive): `Abra`, `Robinhood`, `Uphold`, `Voyager`.
- `-i`, `--input` - *Required*. Full path to input file.
- `-o`, `--output` - *Required*. Folder path to place output file. Folder must already exist.
- `-f`, `--filename` - *Optional*. Filename to name output file. Defaults to `converted-crypto-com-tax-{originalFileName}` eg `converted-crypto-com-tax-2021-robinhood-crypto.csv`.

```shell
CryptoComTax.Console.exe -e robinhood -i ".\2021-robinhood-crypto.csv" -o "."
```

### Supported Exchanges

- Abra (planned)
- Robinhood (basic support; doesn't support transfers)
- Uphold (planned)
- Voyager (currently doesn't support crypto transfer, I didn't have any examples to work with)