using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day16
    {
        private const string InputPath = "input/2021-12-16.txt";

        public int Puzzle1()
        {
            var binaryInputGroups = File.ReadAllText(InputPath)
                                        .Select(GetBinaryStringForHexCharacter);
            var binaryInput = string.Join("", binaryInputGroups);

            var packet = new Packet(binaryInput);
            return packet.GetVersionSum();
        }

        private class Packet
        {
            public int Version { get; private set; }
            private int _packetTypeId;
            private int _lengthTypeId;
            private long _literalValue;
            private List<Packet> _subPackets;
            
            public int BitLength { get; private set; }
            
            public Packet(string binaryInput)
            {
                Version = Convert.ToInt32(binaryInput.Substring(0, 3), 2);
                _packetTypeId = Convert.ToInt32(binaryInput.Substring(3, 3), 2);
                binaryInput = binaryInput.Remove(0, 6);
                BitLength = 6;

                if (_packetTypeId == 4)
                {
                    ParseLiteralValuePacket(binaryInput);
                }
                else
                {
                    _lengthTypeId = Convert.ToInt32(binaryInput.Substring(0, 1), 2);
                    binaryInput = binaryInput.Remove(0, 1);
                    BitLength += 1;

                    if (_lengthTypeId == 0)
                    {
                        int totalSubPacketBitLength = Convert.ToInt32(binaryInput.Substring(0, 15), 2);
                        binaryInput = binaryInput.Remove(0, 15);
                        BitLength += 15;

                        _subPackets = new List<Packet>();
                        int currentSubPacketBitLength = 0;
                        while (currentSubPacketBitLength < totalSubPacketBitLength)
                        {
                            var thisSubPacket = new Packet(binaryInput);
                            currentSubPacketBitLength += thisSubPacket.BitLength;
                            this.BitLength += thisSubPacket.BitLength;
                            binaryInput = binaryInput.Remove(0, thisSubPacket.BitLength);
                            
                            _subPackets.Add(thisSubPacket);
                        }
                    }
                    else
                    {
                        int totalNumSubPackets = Convert.ToInt32(binaryInput.Substring(0, 11), 2);
                        binaryInput = binaryInput.Remove(0, 11);
                        BitLength += 11;

                        _subPackets = new List<Packet>();
                        int currentNumSubPackets = 0;
                        while (currentNumSubPackets < totalNumSubPackets)
                        {
                            var thisSubPacket = new Packet(binaryInput);
                            currentNumSubPackets++;
                            this.BitLength += thisSubPacket.BitLength;
                            binaryInput = binaryInput.Remove(0, thisSubPacket.BitLength);
                            
                            _subPackets.Add(thisSubPacket);
                        }
                    }
                }
            }

            private void ParseLiteralValuePacket(string binaryInput)
            {
                string literalValueBinaryRepresentation = String.Empty;
                bool shouldContinueParsing = true;
                while (shouldContinueParsing)
                {
                    literalValueBinaryRepresentation += binaryInput.Substring(1, 4);
                    shouldContinueParsing = binaryInput.Substring(0, 1) == "1";
                    binaryInput = binaryInput.Remove(0, 5);
                    BitLength += 5;
                }

                _literalValue = Convert.ToInt64(literalValueBinaryRepresentation, 2);
            }

            public int GetVersionSum()
            {
                int subPacketVersionSum = _subPackets?.Sum(p => p.GetVersionSum()) ?? 0;
                return Version + subPacketVersionSum;
            }
        }
        
        private string GetBinaryStringForHexCharacter(char hexCharacter)
        {
            int value = Convert.ToInt32(hexCharacter.ToString(), 16);
            return Convert.ToString(value, 2).PadLeft(4, '0');
        }

    }
}
