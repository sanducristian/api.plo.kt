//
// Exception enums space allocation
//
/*
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * |  Start |  Size | Description 
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * | 0x0000 |    48 | General exceptions 
 * | 0x0030 |    16 | Logging
 * | 0x0040 |    64 | System exceptions
 * | 0x0080 |    64 | Authentification
 * | 0x00C0 |    64 | 
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * | 0x0100 |    64 | Application - base
 * | 0x0140 |    64 | Application - process
 * | 0x0180 |    64 | Application - threads
 * | 0x0180 |    64 | Application - plugins
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * | 0x0200 |    64 | Data errors - singles
 * | 0x0240 |    64 | Data errors - lists
 * | 0x0280 |    64 | Data errors - parameters
 * | 0x0280 |    64 | 
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * | 0x0300 |    64 | File system - Files
 * | 0x0340 |    64 | File system - Directory, Path
 * | 0x0380 |    64 | File system - Drives
 * | 0x03C0 |    64 | Devices
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * | 0x0400 |    64 | Communication - Buffers
 * | 0x0440 |    64 | Communication - Network
 * | 0x0480 |    64 | Communication - Devices
 * | 0x04C0 |    32 | Communication - General and ports
 * | 0x04E0 |    32 | Communication - Packets
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * | 0x0700 |   128 | FTDI device exceptions
 * | 0x0780 |   128 | 
 * | 0x0800 |   128 | Database errors
 * | 0x0880 |   128 | Image handling
 * | 0x0900 |   128 | 
 * | 0x0980 |   128 | 
 * | 0x0A00 |   128 | 
 * | 0x0A80 |   128 | 
 * | 0x0B00 |   128 | 
 * | 0x0B80 |   128 | 
 * | 0x0C00 |   128 | 
 * | 0x0C80 |   128 | 
 * | 0x0D00 |   128 | 
 * | 0x0D80 |   128 | 
 * | 0x0E00 |   128 | 
 * | 0x0E80 |   128 | 
 * | 0x0F00 |   128 | 
 * | 0x0F80 |   128 | 
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * +    External modules and plugins
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * | 0x1000 |   128 | Barcodes errors
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * | 0x7FF0 |    16 | *** reserved ***
 * +--------+-------+----------------------------------------------------------------------------------------------------
 * 
 */

namespace Kt.Kernel;

public enum KtExceptionEnum {
    /// <summary>
    /// General exceptions
    /// </summary>
    Unknown = 0,
    NotInitialized,                 /// The object was not Initialized
    NotImplemented,                 /// The function was not implemented
    NotSupported,                   /// The function requested is not supported
    Obsolete,                       /// The function called is obsolete
    OutOfMemory,                    /// There is no more memory
    Busy,                           /// The Requested resource is executing a command
    InUse,                          /// The requested resource is in use
    Timeout,                        /// The operation had ended with timeout


    //
    // 0x0030 - LOG files exceptions
    //
    LogNotInitialized = 0x0030,     // The loging system was not Initialized


    //
    // 0x0040 - System errors 
    //
    SysFailedRestart = 0x0040,      // The restart operation had failed
    SysFailedShutdown,              // The shutdown operation had failed

    //
    // 0x0060 - Object errors
    //
    ObjectNotFound = 0x0060,        // The object was not found


    //
    // 0x0080 - Authentification errors 
    //
    AuthError = 0x0080,             // Authentification general error (see details)
    AuthUserNotLogedIn,             // The user is not logged in
    AuthUserAlreadyLogedIn,         // The user is already logeed in
    AuthUserNotRoot,                // The user is not root
    AuthInvalidPassword,            // The password is invalid
    AuthInvalidCredentials,         // The credentials are invalid
    AuthCredentialsError,           // There was an error trying to retrieve the credentials
    AuthPasswordExpired,            // The password has expired
    AuthAccountDisabled,            // The specified account is disabled


    //
    // 0x00C0 - Network
    //
    NetUnavailable = 0x00c0,        // The network is not available
    NetHostNotFound,                // The specified host was not found
    NetInvalidServerName,           // The server name is invalid
    NetInvalidPort,                 // The specified port is invalid
    NetInvalidIp,                   // The specified IP is not a valid IP
    NetInvalidIp4,                  // The specified IP is not a valid IP4
    NetInvalidIp6,                  // The specified IP is not a valid IP6
    NetDisconnected,                // The network connection got disconnected
    NetTimeout,                     // A timeout has occured for the current network operation



    //
    // 0x0100 - Application - Base errors
    //		
    AppFailedToStart = 0x0100,      // The application 
    AppFunctionNotFound,            // The specified function could not be found
    AppProcedureNotFound,           // The application procedure could not be found
    AlreadyRunning,                 // The operation is already running
    NotRunning,                     // The operation is not running


    //
    // 0x0140 - Application - Process errors
    //
    ProcCannotCreate = 0x0140,      // The specified process cannot be created
    ProcCannotEnd,                  // Cannot terminate the specified process
    ProcCannotFork,                 // Cannot fork the current process
    ProcMaxThreadsReached,          // No more threads can be created in the system
    ProcModeAlreadyBackground,      // The process is already in background processing mode
    ProcModeNotBackground,          // The process is not in background processing mode


    //
    // 0x0180 - Application - Threads errors
    //
    ThreadCannotCreate = 0x0180,    // Cannot create thread
    ThreadModeAlreadyBackground,    // The thread is already in background processing mode
    ThreadModeNotBackground,        // The thread is not in background processing mode


    //
    // 0x01C0 - Application - Plugins and modules
    //
    AppModuleNotFound = 0x01C0,     // The specified module could not be found
    AppModuleCannotLoad,            // The application module cannot be loaded
    AppModuleInvalid,               // The specified module is invalid and cannot be used by the application
    AppModuleCannotGetType,         // Cannot load the module type in order to identify it


    //
    // 0x0200 - Data errors - singles values 
    //
    DataProtected = 0x0200,         // The object is write protected. No save will be done
    DataLoaded,
    DataHasChanged,
    DataLoadError,
    DataSaveError,
    DataNotFetched,                 // There was no data fetched
    DataReaderError,                // There was a problem with the reader
    DataRowCountInvalid,            // There are a different number of rows as was expected
    DataCannotBeSaved,              // The data cannot be saved (check independent flags)
    DataInvalidSignature,           // The data packet signature is invalid
    DataInvalidChecksum,            // The check sum is invalid
    DataEmpty,                      // There is no data to handle
    DataNotEmpty,                   // There is still data
    DataNull,                       // The data is null, the size and contents are no present
    DataInvalidSize,                // The data size is invalid (the data size does not equals the data present)
    DataSizeInconsistent,           // The data size is inconsistent with what the size should be


    //
    // 0x0240 - Data errors - lists
    //
    ListNull = 0x240,               // The list is null (not allocated)
    ListEmpty,                      // The list is empty
    ListAlreadyMounted,             // The item is already in the list
    ListAlreadyUnMounted,           // The item was in the list
    ListNotMounted,                 // The item was not in the list
    ListItemExists,                 // The item already exist in the list
    ListItemNotFound,               // The item was not found in the list
    ListIndexNegative,              // The list index cannot be negative
    ListIndexOutsideRange,          // The specified index is outside the range of the list
    ListTooFewItem,                 // The list contains too few items
    ListTooManyItem,                // The list contains too many items
    ListRangeOutsideLimits,         // The given range is outside the limits of the object
    ListCountNegative,              // The number of elements cannot be negative
    ListOutOfRange,                 // The value to set requested is out of range
    ListReadOnly,                   // The list is readonly
    ListWriteOnly,                  // The list is write only


    //
    // 0x0260 - Text conversions error - list
    //
    TextConvError = 0x260,          // General error for text conversion
    TextConvErrorEmpty,             // There is no text to be converted
    TextConvErrorToLarge,           // The text converted is to large for the specified data type
    TextConvErrorNegative,          // The text cannot be converted as it is negative
    TextConvErrorPositive,          // The text cannot be converted as it is positive
    TextConvErrorInvalidChar,       // The text to be converted contains invalid chars


    // 
    // 0x0280 - Data errors - Parameters related values
    //
    ParamNull = 0x0280,             // The passed parameter is null (for pointers, classed, etc)
    ParamEmpty,                     // The parameter is empty (for strings)
    ParamInvalid,                   // The specified paramter is invalid
    ParamValueSize,                 // Invalid size for parameter
    ParamIdNull,                    // The parameter id is null
    ParamTooSmall,                  // The parameter is too big
    ParamTooBig,                    // The value of the parameter is too big
    ParamTooLarge,                  // The parameter number of elements is too large (too many elements)


    //
    // 0x0300 - File system - File errors exceptions
    //
    FsFileDoesNotExist = 0x0300,    // The specified file does not exit
    FsFileAccessDenied,             // You don't have enough access rights to open the file
    FsFileLocked,                   // The process cannot access the file because another process has locked a portion of the file
    FsFileNotLocked,                // The file is not locked
    FsFileInUse,                    // The specified file is in used by another user or application
    FsFileNotInUse,                 // The spciefied file is not in use by any other process
    FsFileExist,                    // The file already exists
    FsFileReadOnly,                 // The file is readonly
    FsFileWriteOnly,                // The file can only be written (as on a tape or pipe)
    FsFileCannotCreate,             // Cannot create the specified file
    FsFileCannotOpen,               // Cannot open the specified file
    FsFileModified,                 // The file was modified
    FsFileEmpty,                    // The file is empty
    FsFileTooBig,                   // The file is too big for the filesystem
    FsFileBOF,                      // The begin of the file was reached
    FsFileEOF,                      // The end of the file was reached
    FsFileEOL,                      // The end of line was reached
    FsFileBOFUnexpected,            // Unexpected Begin Of File was found		
    FsFileEOFUnexpected,            // Unexpected End Of File was found
    FsFileEOLUnexpected,            // Unexpected End Of Line was found
    FsFileNegativeSeek,             // An attempt was made to move the file pointer before the beginning of the file
    FsFileCasesensitive,            // The filename is casesensitive
    FsFileNameInvalidCharacters,    // There are invalid characters in the filename
    FsFileNameTooLong,              // The file name is too long
    FsFileInvalid,                  // The specified file is invalid
    FsFileTooLarge,                 // The file size exceeds the limit allowed and cannot be saved
    FsFileNameNotSet,               // The file name was not set


    //
    // 0x0340 - File system - Directory errors
    //
    FsDirectoryDoesNotExist = 0x0340,// The specified directory does not exist
    FsDirectoryCannotCreate,        // Cannot create the specified directory
    FsDirectoryCannotRemove,        // The directory cannot be removed
    FsDirectoryCurrent,             // Cannot remove the current directory
    FsDirectoryParse,               // Cannot parse the specified directory
    FsDirectoryNotEmpty,            // The directory is not empty
    FsDirectoryNotRoot,             // the directory is not a root
    FsDirectoryTooManyFiles,        // The directory contains too many files
    FsDirectoryPathTooLong,         // The directory path is too long
    FsDirectoryNameInvalid,         // The directory name is invalid
    FsDirectoryNotSupported,        // An operation is not supported on a directory


    //
    // 0x0380 - File system - Drive errors
    //
    FsDriveDoesNotExist = 0x0380,   // The drive does not exist
    FsDriveInvalid,                 // The drive is invalid
    FsDriveBad,                     // The drive does not work property
    FsDriveSectorNotFound,          // The drive sector cannot be found
    FsDriveWrong,                   // The drive is not the good one, please inset the good one
    FsDriveFull,                    // The drive is full
    FsDriveInUse,                   // The drive is in use
    FsDriveBusy,                    // The drive is executing a command
    FsDriveNoLabel,                 // The drive has no label
    FsDriveTooFragmented,           // The drive is too fragmented


    //
    // 0x03C0 - File system - Device errors
    //
    FdDeviceNotReady = 0x03C0,      // The device or drive is not ready
    FdDeviceBadCommand,             // The device or drive does not recognize the command
    FdDeviceNotIdentical,           // The device or drive is not the same
    FdDeviceReadError,              // The system cannot read from the specified device
    FdDeviceWriteError,             // The device cannot write to the specified device
    FdDeviceGeneralFailure,         // The device is not functioning
    FdDeviceDoesNotExist,           // The specified device does not exists
    FdDeviceOpenFailed,             // The system cannot open the device or file
    FdDeviceNegativeSeek,           // Cannot seak beyong the begin of device data
    FdDeviceCannotSeek,             // Cannot seek on the specified device
    FdDeviceUnreachable,            // The specified device is unreacheable
    FdDeviceBusy,                   // The device is busy (executing a command)
    FdDeviceInUse,                  // The device is in use (used by another process)


    //
    // 0x0400 - Communication - Buffer
    //
    BufferEmpty = 0x0400,           // The buffer is empty
    BufferFull,                     // The buffer is full
    BufferOverflow,                 // The buffer had overflowed, data can be lost
    BufferInsufficient,             // There is insufficient buffer space


    //
    // 0x04C0 - The communication related exceptions - Ports
    //
    PortAlreadyOpen = 0x04C0,       // The port is already opened
    PortInUse,                      // The port is already in use
    PortNotOpen,                    // The port is not opened
    PortNotFound,


    //
    // 0x04D0 - The communication related exceptions - Ports
    //
    CommTimeout = 0x04D0,           // Communication timeout occurred
    CommInvalidProtocol,            // The protocol used is invalid


    //
    // 0x04E0 - The communication related exceptions - Ports
    //
    PacketCrcError = 0x04E0,        // Communication error (CRC error)
    PacketIncomplete,               // The received packet is incomplete


    /// <summary>
    /// FDTI Serial devices
    /// </summary>
    FtdiUnableToCount = 0x0700,     // Unable to count the connected devices
    FtdiUnableToListDevs,           // Failed to get the list of devices
    FtdiFailedToOpenDevice,         // Failed to open the device
    FtdiFailedToSetBaud,            // Failed to set up the baud rate for the devices
    FtdiFailedToSetBusConfig,       // Failed to set up the bus configuration (data bits, stop bits and parity)
    FtdiFailedToSetFlowControl,     // Failed to set up flow control
    FtdiFailedToSetTimeouts,        // Failed to set up the timeouts (read and write)
    FtdiFailedToWrite,              // Failed to write to the device
    FtdiFailedToRead,               // Failed to read from the device
    FtdiFailedToReadBufSize,        // Failed to read the buffer size of incomming data


    /// <summary>
    /// Database related expceptions
    /// </summary>
    DBConnectionAlreadyCreated = 0x0800,        // The connection object to the database was already been created
    DBConnectionAlreadyConnected,   // The DB Connection is already present
    DBNotConnected,                 // There is no connection with the database
    DBConnectionNotPossible,        // Unable to connect to the database
    DBQueryEvaluationError,
    DBCommandNotInitialized,        // The command to be executed was not Initialized
    DBTableNameEmpty,               // The table name is empty
    DBExecuteError,                 // Failed to execute command
    DBUpdateError,                  // Failed to execute update command
    DBCreateIdError,                // Failed to create an id
    DBConversionError,              // Failed to convert DB Data type
    DBReaderNotInitialized,         // The database reader is not inizialized
    DBColumnNameEmpty,              // The column name is empty




    //
    // 0x1000 - Barcode related exceptions
    //
    BarcodeErrorUnknown = 0x1000,
    BarcodeNotInitialized,          // The barcode was not Initialized
    BarcodeUnknownEncoding,         // The encoding is unknwon
    BarcodeEncodingNotDefined,      // The encoding was not specified
    BarcodeInvalidCharForEncoding,  // The specified character cannot be encoded
    BarcodeInvalidNumber,
    BarcodeTooFew,                  // There are not enough numbers for this barcode
    BarcodeTooShort,
    BarcodeTooMany,                 // There are too many numbers for this barcode
    BarcodeTooLong,
    BarcodeCannotFindSize,          // Cannot find suitable size, barcode too long
    BarcodeImageInvalidSize,        // Invalid image size [W]x[H]
    BarcodeImageTooLarge,           // Barcode too long for [W]x[H]
    BarcodeCannotFit,               // Cannot fit the barcode inside the image [W]x[H]
    BarcodeScannerNotInitialized,   // The scanner is not Initialized
    BarcodeScannerCommNotOpen,      // The communication with the scanner is closed (serial port not open)
    BarcodeScannerCannotOpen,       // Cannot access the port
    BarcodeDecodingFailed,          // The decoding of the barcode failed (data cannot be extracted as the format is invalid)


    //
    // 0x1100 - Compression
    //
    CompressGeneralException,       // A general exception for the compression module. Please see details
    CompressNeedDictionary,         // A dictionary is required
    CompressCannotDeflate,          // Cannot deflate all inputs
};
