using Nethereum.Contracts;
using Nethereum.Web3;

namespace PancakeSwapNET
{
    public class Factory
    {
        private readonly Web3 _web3;
        private readonly Contract _contract;
        public readonly string ContractAddress;

        private const string _contractAbi = """
                        [
              {
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "_feeToSetter",
                    "type": "address"
                  }
                ],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "constructor"
              },
              {
                "anonymous": false,
                "inputs": [
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "token0",
                    "type": "address"
                  },
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "token1",
                    "type": "address"
                  },
                  {
                    "indexed": false,
                    "internalType": "address",
                    "name": "pair",
                    "type": "address"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "name": "PairCreated",
                "type": "event"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "INIT_CODE_PAIR_HASH",
                "outputs": [
                  {
                    "internalType": "bytes32",
                    "name": "",
                    "type": "bytes32"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "name": "allPairs",
                "outputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "allPairsLength",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "tokenA",
                    "type": "address"
                  },
                  {
                    "internalType": "address",
                    "name": "tokenB",
                    "type": "address"
                  }
                ],
                "name": "createPair",
                "outputs": [
                  {
                    "internalType": "address",
                    "name": "pair",
                    "type": "address"
                  }
                ],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "feeTo",
                "outputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "feeToSetter",
                "outputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  },
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "name": "getPair",
                "outputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "_feeTo",
                    "type": "address"
                  }
                ],
                "name": "setFeeTo",
                "outputs": [],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "_feeToSetter",
                    "type": "address"
                  }
                ],
                "name": "setFeeToSetter",
                "outputs": [],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              }
            ]
            """;

        public Factory(Web3 web3, string address)
        {
            _web3 = web3;
            ContractAddress = address;
            _contract = _contract = _web3.Eth.GetContract(_contractAbi, ContractAddress);
        }

        public async Task<string> FeeToAsync()
        {
            Function function = _contract.GetFunction("feeTo");
            return await function.CallAsync<string>();
        }

        public async Task<string> FeeToSetterAsync()
        {
            Function function = _contract.GetFunction("feeToSetter");
            return await function.CallAsync<string>();
        }

        public async Task<int> GetAllPairsLengthAsync()
        {
            Function function = _contract.GetFunction("allPairsLength");
            return await function.CallAsync<int>();
        }

        public async Task<string> GetPairAddressByTokensAsync(string token0Address, string token1Address)
        {
            Function function = _contract.GetFunction("getPair");
            string address = await function.CallAsync<string>(token0Address, token1Address);
            if (address == "0x0000000000000000000000000000000000000000")
                throw new Exception("Incorrect token addresses");
            return address;
        }
    }
}