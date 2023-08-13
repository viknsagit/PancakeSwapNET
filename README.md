# PancakeSwapNET Library Documentation

The PancakeSwapNET library provides a set of classes and methods for interacting with the PancakeSwap decentralized exchange on the Ethereum blockchain.
Library using [Netherium](https://github.com/Nethereum) to work with PancakeSwap contract functions.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Classes](#classes)
  - [Router](#router)
- [Methods](#methods)
  - [Constructor](#constructor)
  - [GetPairAsync](#getpairasync)
  - [GetAmountsOutAsync](#getamountsoutasync)
  - [GetAmountOutAsync](#getamountoutasync)
  - [GetAmountInAsync](#getamountinasync)
  - [GetAmountsInAsync](#getamountsinasync)
  - [GetWETHAddressAsync](#getwethaddressasync)
  - [GetFactoryAddressAsync](#getfactoryaddressasync)
  - [SwapExactETHForTokensAsync](#swapexactethfortokensasync)
  - [SwapExactTokensForETHAsync](#swapexacttokensforethasync)
  - [SwapTokensForExactETHAsync](#swaptokensforexactethasync)
  - [SwapExactTokensForETHSupportingFeeOnTransferTokensAsync](#swapexacttokensforethsupportingfeeontransfertokensasync)
  - [SwapExactTokensForTokensAsync](#swapexacttokensfortokensasync)
  - [SwapExactTokensForTokensManualAsync](#swapexacttokensfortokensmanualasync)

## Installation

To use the PancakeSwapNET library, make sure you have the Nethereum package installed in your project. You can install it using NuGet:

```bash
Install-Package Nethereum.Web3
```

## Usage

First, import the required namespaces:

```csharp
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Numerics;
using System.Threading.Tasks;
```

Then, you can use the PancakeSwapNET library to interact with the PancakeSwap decentralized exchange:

```csharp
string chainURL = "YOUR_BLOCKCHAIN_URL";
string contractAddress = "PANCAKESWAP_CONTRACT_ADDRESS";
string privateKey = "YOUR_PRIVATE_KEY";

Router router = new Router(chainURL, contractAddress, privateKey);

// Use the router's methods for various interactions with PancakeSwap
```

## Classes

### Router

The `Router` class is the main class of the PancakeSwapNET library. It provides methods to interact with the PancakeSwap decentralized exchange.

#### Constructor

The `Router` class has several constructors that allow you to initialize the class with different parameters. Each constructor sets up the necessary connections and configurations for interacting with PancakeSwap.

```csharp
Router(string chainURL, string contractAddress, string primaryKey)
Router(string chainURL, string contractAddress)
Router(string chainURL, string contractAddress, string primaryKey, GasSettings gasSettings)
Router(string chainURL, string contractAddress, GasSettings gasSettings)
```

- `chainURL`: URL of the blockchain network.
- `contractAddress`: Address of the PancakeSwap contract.
- `primaryKey`: Private key of the account to use for transactions (optional).
- `gasSettings`: Gas settings for transactions (optional).

### Methods

#### GetPairAsync

```csharp
Task<Pair> GetPairAsync(string pairAddress)
Task<Pair> GetPairAsync(string token0Address, string token1Address)
```

- Returns a `Pair` object representing the pair of tokens at the specified addresses.

#### GetAmountsOutAsync

```csharp
Task<List<BigInteger>> GetAmountsOutAsync(BigInteger amountToSell, string tokenAddress, string tokenAddress1)
Task<List<BigInteger>> GetAmountsOutAsync(BigInteger amountToSell, string tokenAddress)
```

- Returns the relationship of one token to another token after selling a specified amount.

#### GetAmountOutAsync

```csharp
Task<BigInteger> GetAmountOutAsync(BigInteger amountIn, BigInteger reserveIn, BigInteger reserveOut)
```

- Returns the amount out for a given amount in, reserve in, and reserve out.

#### GetAmountInAsync

```csharp
Task<BigInteger> GetAmountInAsync(BigInteger amountOut, BigInteger reserveIn, BigInteger reserveOut)
```

- Returns the amount in for a given amount out, reserve in, and reserve out.

#### GetAmountsInAsync

```csharp
Task<List<BigInteger>> GetAmountsInAsync(BigInteger amount, string tokenAddress, string tokenAddress1)
```

- Returns the relationship of one token to another token after buying a specified amount.

#### GetWETHAddressAsync

```csharp
Task<string> GetWETHAddressAsync()
```

- Returns the address of the WETH token.

#### GetFactoryAddressAsync

```csharp
Task<string> GetFactoryAddressAsync()
```

- Returns the address of the PancakeSwap factory contract.

#### SwapExactETHForTokensAsync

```csharp
Task<TransactionReceipt> SwapExactETHForTokensAsync(decimal amountInEth, string tokenAddress)
```

- Swaps exact ETH for as many output tokens as possible.

#### SwapExactTokensForETHAsync

```csharp
Task<TransactionReceipt> SwapExactTokensForETHAsync(decimal amountInEth, string tokenAddress)
```

- Swaps exact tokens for as much ETH as possible.

#### SwapTokensForExactETHAsync

```csharp
Task<TransactionReceipt> SwapTokensForExactETHAsync(decimal amountInEth, string tokenAddress)
```

- Swaps tokens for exact ETH.

#### SwapExactTokensForETHSupportingFeeOnTransferTokensAsync

```csharp
Task<TransactionReceipt> SwapExactTokensForETHSupportingFeeOnTransferTokensAsync(decimal amountInEth, string tokenAddress)
```

- Swaps tokens for ETH, supporting tokens that take a fee on transfer.

#### SwapExactTokensForTokensAsync

```csharp
Task<TransactionReceipt> SwapExactTokensForTokensAsync(decimal amountIn, string tokenAddress, string tokenAddress1)
```

- Swaps exact tokens for as many output tokens as possible.

#### SwapExactTokensForTokensManualAsync

```csharp
Task<TransactionReceipt> SwapExactTokensForTokensManualAsync(BigInteger amountsIn, BigInteger amountsOut, string tokenAddress, string tokenAddress1)
```

- Executes a swap of tokens for tokens with exact amounts specified.

#### DecimalToWei

```csharp
private static BigInteger DecimalToWei(decimal amount)
```

- Converts a decimal amount to Wei.

---

## Factory Class

The `Factory` class provides methods to interact with the PancakeSwap Factory contract on the Ethereum blockchain. The Factory contract is used to manage pairs of tokens on the PancakeSwap decentralized exchange.

### Table of Contents

- [Constructor](#constructor)
- [Methods](#methods)
  - [FeeToAsync](#feetoasync)
  - [FeeToSetterAsync](#feetosetterasync)
  - [GetAllPairsLengthAsync](#getallpairslengthasync)
  - [GetPairAddressByTokensAsync](#getpairaddressbytokensasync)

### Constructor

```csharp
Factory(Web3 web3, string address)
```

- `web3`: An instance of the `Web3` class.
- `address`: The address of the PancakeSwap Factory contract.

### Methods

#### FeeToAsync

```csharp
Task<string> FeeToAsync()
```

- Asynchronously retrieves the address set as the 'feeTo' value.

#### FeeToSetterAsync

```csharp
Task<string> FeeToSetterAsync()
```

- Asynchronously retrieves the address set as the 'feeToSetter' value.

#### GetAllPairsLengthAsync

```csharp
Task<int> GetAllPairsLengthAsync()
```

- Asynchronously retrieves the length of all pairs managed by the Factory contract.

#### GetPairAddressByTokensAsync

```csharp
Task<string> GetPairAddressByTokensAsync(string token0Address, string token1Address)
```

- Asynchronously retrieves the address of the pair created using the specified token addresses.
- `token0Address`: The address of the first token.
- `token1Address`: The address of the second token.

Returns the address of the pair. If the address is "0x0000000000000000000000000000000000000000," an exception is thrown indicating incorrect token addresses.

---

## Pair Class

The `Pair` class provides methods to interact with the PancakeSwap Pair contract on the Ethereum blockchain. The Pair contract represents a liquidity pair on the PancakeSwap decentralized exchange.

### Table of Contents

- [Constructor](#constructor)
- [Methods](#methods)
  - [InitPair](#initpair)
  - [GetPrice0CumulativeLastAsync](#getprice0cumulativelastasync)
  - [GetPrice1CumulativeLastAsync](#getprice1cumulativelastasync)
  - [GetkLastAsync](#getklastasync)
  - [GetReservesAsync](#getreservesasync)
  - [CalculatePriceImpactAsync](#calculatepriceimpactasync)

### Constructor

```csharp
Pair(Web3 web3)
```

- `web3`: An instance of the `Web3` class.

### Methods

#### InitPair

```csharp
Task InitPair(string pairAddress)
```

- Initializes a new instance of the `Pair` class with the given pair address.
- `pairAddress`: The address of the pair contract.

#### GetPrice0CumulativeLastAsync

```csharp
Task<BigInteger> GetPrice0CumulativeLastAsync()
```

- Asynchronously retrieves the cumulative last price of the 0th asset.

#### GetPrice1CumulativeLastAsync

```csharp
Task<BigInteger> GetPrice1CumulativeLastAsync()
```

- Asynchronously retrieves the cumulative last price of the 1st asset.

#### GetkLastAsync

```csharp
Task<BigInteger> GetkLastAsync()
```

- Asynchronously retrieves the last k value from the contract.

#### GetReservesAsync

```csharp
Task<GetReservesFunc> GetReservesAsync()
```

- Asynchronously retrieves the reserves of the liquidity pair using the `getReserves` function on the contract.
- Returns a `GetReservesFunc` object containing the result of the function call.

#### CalculatePriceImpactAsync

```csharp
Task<decimal[]> CalculatePriceImpactAsync(decimal percent, int firstTokenDecimals = 18, int secondTokenDecimals = 18)
```

- Deducts the price impact from the specified percentage and calculates the price impact and number of tokens to exchange for each token.
- `percent`: The percentage (0.01 = 1%).
- `firstTokenDecimals`: The number of decimals for the first token (default: 18).
- `secondTokenDecimals`: The number of decimals for the second token (default: 18).
- Returns an array of decimals: `[price_impact_trade_token, amount_traded_token, price_impact_trade_token1, amount_traded_token1]`.

---

Давайте продолжим документацию для класса `GasSettings` из вашей библиотеки:

## GasSettings Class

The `GasSettings` class represents gas-related settings that can be used in transactions on the Ethereum blockchain.

### Table of Contents

- [Constructor](#constructor)
- [Properties](#properties)

### Constructor

```csharp
GasSettings(BigInteger gasLimitInWei, BigInteger gasPrice)
```

- Creates an instance of the `GasSettings` class with the specified gas limit and gas price in Wei.
- `gasLimitInWei`: The gas limit in Wei.
- `gasPrice`: The gas price in Wei.

```csharp
GasSettings(int gasLimit, BigInteger gasPrice)
```

- Creates an instance of the `GasSettings` class with the specified gas limit and gas price in Gwei.
- `gasLimit`: The gas limit in Gwei.
- `gasPrice`: The gas price in Wei.

### Properties

#### `gasLimit`

- A `BigInteger` representing the gas limit for the transaction.

#### `gasPrice`

- A `BigInteger` representing the gas price for the transaction.

---
