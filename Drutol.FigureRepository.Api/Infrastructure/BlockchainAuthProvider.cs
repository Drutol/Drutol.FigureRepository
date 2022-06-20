using System.Numerics;
using Nethereum.Signer.EIP712;

namespace Drutol.FigureRepository.Api.Infrastructure;

public class BlockchainAuthProvider
{
    private void Lol()
    {
        var signer = new Eip712TypedDataSigner();
        var message = new Owner
        {
            ownerAddress = "0xcb95735cb04e3c0693925e2396b92acdb57e88c2"
        };

        var recover = signer.RecoverFromSignatureV4(
            message,
            GetOwnerTypedDefinition(),
            signed);

        var web3 = new Web3();
        var contract = web3.Eth.GetContract(Abi, ContractAddress);
        var result = await contract.GetFunction("balanceOf").CallAsync<BigInteger>(recover);
        var uri = await contract.GetFunction("tokenURI").CallAsync<string>(123456);


    }
}