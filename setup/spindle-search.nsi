!include "MUI.nsh"

# TODO: version number should be specified once in whole source tree
outFile "SpindleSearch-1.4.0.3.exe"

Name "Spindle Search 1.4.0.3"

InstallDir "$PROGRAMFILES\Spindle Search"

!define MUI_ICON "..\res\Spindle.ico"
!define MUI_UNICON "..\res\Spindle.ico"
!define MUI_FINISHPAGE_RUN "$INSTDIR\SpindleSearch.exe"

!insertMacro MUI_PAGE_WELCOME
!insertMacro MUI_PAGE_LICENSE "..\doc\LICENSE.txt"
!insertMacro MUI_PAGE_COMPONENTS
!insertMacro MUI_PAGE_DIRECTORY
!insertMacro MUI_PAGE_INSTFILES
!insertMacro MUI_PAGE_FINISH

!insertMacro MUI_LANGUAGE "English"

section
	Call IsDotNETInstalled
	Pop $R3
	Strcmp $R3 0 +3
	Goto +3
       	messagebox MB_OK "This application requires Microsoft .NET Framework.  Please install it and re-run this setup."
	Abort

	ClearErrors
       	readregstr $1 HKLM "SOFTWARE\Google\Google Desktop" "installed_version"
	IfErrors 0 +3
	messagebox MB_OK "This application requires Google Desktop.  Please install it and re-run this setup."
	Abort

	setOutPath "$INSTDIR"

	file "..\doc\CHANGELOG.txt"
	file "..\doc\README.txt"
	file "..\doc\LICENSE.txt"
	file "..\bin\Release\GoogleDesktopWrapper.dll"
	file "..\bin\Release\ICSharpCode.SharpZipLib.dll"
	file "..\bin\Release\Interop.GoogleDesktopAPILib.dll"
	file "..\bin\Release\SpindleSearch.exe"

	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "DisplayName" "Spindle Search"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "UninstallString" "$\"$INSTDIR\uninst.exe$\""
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "InstallLocation" "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "HelpLink" "http://code.google.com/p/spindle-search/"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "URLUpdateInfo" "http://code.google.com/p/spindle-search/"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "URLInfoAbout" "http://code.google.com/p/spindle-search/"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "DisplayVersion" "1.3.0"
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "VersionMajor" 1
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "VersionMinor" 3
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "NoModify" 1
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch" \
		    	 "NoRepair" 1
	# TODO: quiet uninstall, DisplayIcon

	writeUninstaller "$INSTDIR\uninst.exe"

	ExecWait '"$INSTDIR\SpindleSearch.exe" -register'
sectionEnd

Section "Create Start Menu shortcut" createStartMenuShortcut
	# TODO: current user vs. all users
	createShortCut "$SMPROGRAMS\Spindle Search.lnk" "$INSTDIR\SpindleSearch.exe"
SectionEnd

Section /o "Create Desktop shortcut" createDesktopShortcut
	createShortCut "$DESKTOP\Spindle Search.lnk" "$INSTDIR\SpindleSearch.exe"
SectionEnd

section "Uninstall"
	delete "$INSTDIR\uninst.exe"

	ExecWait '"$INSTDIR\SpindleSearch.exe" -unregister'

	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SpindleSearch"

	delete "$SMPROGRAMS\Spindle Search.lnk"
	delete "$DESKTOP\Spindle Search.lnk"

	delete "$INSTDIR\CHANGELOG.txt"
	delete "$INSTDIR\README.txt"
	delete "$INSTDIR\LICENSE.txt"
	delete "$INSTDIR\GoogleDesktopWrapper.dll"
	delete "$INSTDIR\ICSharpCode.SharpZipLib.dll"
	delete "$INSTDIR\Interop.GoogleDesktopAPI2.dll"
	delete "$INSTDIR\SpindleSearch.exe"

	# TODO: delete $INSTDIR if empty
sectionEnd

LangString DESC_createStartMenuShortcut ${LANG_ENGLISH} \
	   "If checked, a Start Menu item will be created for Spindle Search"
LangString DESC_createDesktopShortcut ${LANG_ENGLISH} \
	   "If checked, a shortcut for Spindle Search will be created on desktop"

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
	     !insertmacro MUI_DESCRIPTION_TEXT \
	     		  ${createStartMenuShortcut} $(DESC_createStartMenuShortcut)
	     !insertmacro MUI_DESCRIPTION_TEXT \
	     		  ${createDesktopShortcut} $(DESC_createDesktopShortcut)
!insertmacro MUI_FUNCTION_DESCRIPTION_END


Function IsDotNETInstalled
  Push $0
  Push $1
  Push $2
  Push $3
  Push $4
 
  ReadRegStr $4 HKEY_LOCAL_MACHINE \
    "Software\Microsoft\.NETFramework" "InstallRoot"
  # remove trailing back slash
  Push $4
  Exch $EXEDIR
  Exch $EXEDIR
  Pop $4
  # if the root directory doesn't exist .NET is not installed
  IfFileExists $4 0 noDotNET
 
  StrCpy $0 0
 
  EnumStart:
 
    EnumRegKey $2 HKEY_LOCAL_MACHINE \
      "Software\Microsoft\.NETFramework\Policy"  $0
    IntOp $0 $0 + 1
    StrCmp $2 "" noDotNET
 
    StrCpy $1 0
 
    EnumPolicy:
 
      EnumRegValue $3 HKEY_LOCAL_MACHINE \
        "Software\Microsoft\.NETFramework\Policy\$2" $1
      IntOp $1 $1 + 1
       StrCmp $3 "" EnumStart
        IfFileExists "$4\$2.$3" foundDotNET EnumPolicy
 
  noDotNET:
    StrCpy $0 0
    Goto done
 
  foundDotNET:
    StrCpy $0 1
 
  done:
    Pop $4
    Pop $3
    Pop $2
    Pop $1
    Exch $0
FunctionEnd
